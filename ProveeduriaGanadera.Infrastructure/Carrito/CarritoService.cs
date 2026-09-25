using Microsoft.EntityFrameworkCore;
using ProveeduriaGanadera.Application.Carrito;
using ProveeduriaGanadera.Domain.Entities;
using ProveeduriaGanadera.Domain.Enums;
using ProveeduriaGanadera.Infrastructure.Persistence;

namespace ProveeduriaGanadera.Infrastructure.Carrito;

public class CarritoService : ICarritoService
{
    private readonly IDbContextFactory<ProveeduriaGanaderaDbContext> _dbContextFactory;

    public CarritoService(IDbContextFactory<ProveeduriaGanaderaDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<CarritoDto> ObtenerAsync(int usuarioId)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        var pedido = await BuscarCarritoAsync(db, usuarioId);
        return pedido is null ? new CarritoDto() : Mapear(pedido);
    }

    public async Task<CarritoDto> AgregarAsync(int usuarioId, int productoId, int cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cantidad));
        }

        await using var db = await _dbContextFactory.CreateDbContextAsync();
        var pedido = await BuscarCarritoAsync(db, usuarioId) ?? await CrearCarritoAsync(db, usuarioId);

        var item = pedido.Items.FirstOrDefault(i => i.ProductoId == productoId);
        if (item is not null)
        {
            item.Cantidad += cantidad;
        }
        else
        {
            var precioVigente = await db.HistorialesDePrecio
                .Where(h => h.ProductoId == productoId)
                .OrderByDescending(h => h.VigenteDesde)
                .Select(h => h.Monto)
                .FirstOrDefaultAsync();

            pedido.Items.Add(new ItemPedido
            {
                ProductoId = productoId,
                Cantidad = cantidad,
                PrecioUnitario = precioVigente,
            });
        }

        await db.SaveChangesAsync();
        return Mapear(await CargarConDetalleAsync(db, pedido.Id));
    }

    public async Task<CarritoDto> ActualizarCantidadAsync(int usuarioId, int itemPedidoId, int cantidad)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        var pedido = await BuscarCarritoAsync(db, usuarioId);
        var item = pedido?.Items.FirstOrDefault(i => i.Id == itemPedidoId);
        if (pedido is null || item is null)
        {
            return pedido is null ? new CarritoDto() : Mapear(pedido);
        }

        if (cantidad <= 0)
        {
            db.ItemsPedido.Remove(item);
        }
        else
        {
            item.Cantidad = cantidad;
        }

        await db.SaveChangesAsync();
        return Mapear(await CargarConDetalleAsync(db, pedido.Id));
    }

    public async Task<CarritoDto> QuitarAsync(int usuarioId, int itemPedidoId)
        => await ActualizarCantidadAsync(usuarioId, itemPedidoId, 0);

    public async Task<int> ContarItemsAsync(int usuarioId)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        return await db.ItemsPedido
            .Where(i => i.Pedido.UsuarioId == usuarioId && i.Pedido.Estado == EstadoPedido.Carrito)
            .SumAsync(i => (int?)i.Cantidad) ?? 0;
    }

    private static async Task<Pedido?> BuscarCarritoAsync(ProveeduriaGanaderaDbContext db, int usuarioId)
    {
        var pedidoId = await db.Pedidos
            .Where(p => p.UsuarioId == usuarioId && p.Estado == EstadoPedido.Carrito)
            .Select(p => (int?)p.Id)
            .FirstOrDefaultAsync();

        return pedidoId is null ? null : await CargarConDetalleAsync(db, pedidoId.Value);
    }

    private static async Task<Pedido> CargarConDetalleAsync(ProveeduriaGanaderaDbContext db, int pedidoId)
    {
        return await db.Pedidos
            .Include(p => p.Items)
                .ThenInclude(i => i.Producto)
                    .ThenInclude(pr => pr.Imagenes)
            .FirstAsync(p => p.Id == pedidoId);
    }

    private static async Task<Pedido> CrearCarritoAsync(ProveeduriaGanaderaDbContext db, int usuarioId)
    {
        var clienteId = await db.Usuarios
            .Where(u => u.Id == usuarioId)
            .SelectMany(u => u.Clientes)
            .Select(c => (int?)c.Id)
            .FirstOrDefaultAsync();

        if (clienteId is null)
        {
            throw new InvalidOperationException(
                $"El usuario {usuarioId} no tiene un Cliente asociado, no se puede armar un carrito.");
        }

        var pedido = new Pedido
        {
            Canal = CanalPedido.Online,
            Estado = EstadoPedido.Carrito,
            Fecha = DateTime.UtcNow,
            UsuarioId = usuarioId,
            ClienteId = clienteId.Value,
        };

        db.Pedidos.Add(pedido);
        await db.SaveChangesAsync();
        return pedido;
    }

    private static CarritoDto Mapear(Pedido pedido)
    {
        return new CarritoDto
        {
            PedidoId = pedido.Id,
            Items = pedido.Items.Select(i => new ItemCarritoDto
            {
                Id = i.Id,
                ProductoId = i.ProductoId,
                ProductoNombre = i.Producto.Nombre,
                ImagenUrl = i.Producto.Imagenes.OrderBy(img => img.Orden).Select(img => img.RutaArchivo).FirstOrDefault(),
                Cantidad = i.Cantidad,
                PrecioUnitario = i.PrecioUnitario,
            }).ToList(),
        };
    }
}
