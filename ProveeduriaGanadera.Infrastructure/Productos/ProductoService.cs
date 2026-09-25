using Microsoft.EntityFrameworkCore;
using ProveeduriaGanadera.Application.Common;
using ProveeduriaGanadera.Application.Productos;
using ProveeduriaGanadera.Domain.Entities;
using ProveeduriaGanadera.Infrastructure.Persistence;

namespace ProveeduriaGanadera.Infrastructure.Productos;

public class ProductoService : IProductoService
{
    private readonly ProveeduriaGanaderaDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IArchivoAlmacenamientoService _archivoAlmacenamiento;

    public ProductoService(
        ProveeduriaGanaderaDbContext dbContext,
        ICurrentUserService currentUserService,
        IArchivoAlmacenamientoService archivoAlmacenamiento)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _archivoAlmacenamiento = archivoAlmacenamiento;
    }

    public async Task<List<ProductoDto>> ListarAsync()
    {
        return await _dbContext.Productos
            .OrderBy(p => p.Nombre)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                UnidadDeMedida = p.UnidadDeMedida,
                StockActual = p.StockActual,
                Activo = p.Activo,
                AlicuotaIva = p.AlicuotaIva,
                CodigoDeBarras = p.CodigoDeBarras,
                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categoria.Nombre,
                EspecieIds = p.Especies.Select(e => e.Id).ToList(),
                EspecieNombres = p.Especies.Select(e => e.Nombre).ToList(),
                PrecioActual = p.HistorialDePrecios
                    .OrderByDescending(h => h.VigenteDesde)
                    .Select(h => h.Monto)
                    .FirstOrDefault(),
                Imagenes = p.Imagenes
                    .OrderBy(i => i.Orden)
                    .Select(i => new ImagenProductoDto { Id = i.Id, RutaArchivo = i.RutaArchivo, Orden = i.Orden })
                    .ToList(),
            })
            .ToListAsync();
    }

    public async Task<ProductoDto?> ObtenerAsync(int id)
    {
        return await _dbContext.Productos
            .Where(p => p.Id == id)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                UnidadDeMedida = p.UnidadDeMedida,
                StockActual = p.StockActual,
                Activo = p.Activo,
                AlicuotaIva = p.AlicuotaIva,
                CodigoDeBarras = p.CodigoDeBarras,
                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categoria.Nombre,
                EspecieIds = p.Especies.Select(e => e.Id).ToList(),
                EspecieNombres = p.Especies.Select(e => e.Nombre).ToList(),
                PrecioActual = p.HistorialDePrecios
                    .OrderByDescending(h => h.VigenteDesde)
                    .Select(h => h.Monto)
                    .FirstOrDefault(),
                Imagenes = p.Imagenes
                    .OrderBy(i => i.Orden)
                    .Select(i => new ImagenProductoDto { Id = i.Id, RutaArchivo = i.RutaArchivo, Orden = i.Orden })
                    .ToList(),
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ProductoDto> CrearAsync(ProductoDto producto)
    {
        var especies = await _dbContext.Especies
            .Where(e => producto.EspecieIds.Contains(e.Id))
            .ToListAsync();

        var entidad = new Producto
        {
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            UnidadDeMedida = producto.UnidadDeMedida,
            StockActual = producto.StockActual,
            Activo = producto.Activo,
            AlicuotaIva = producto.AlicuotaIva,
            CodigoDeBarras = string.IsNullOrWhiteSpace(producto.CodigoDeBarras) ? null : producto.CodigoDeBarras,
            CategoriaId = producto.CategoriaId,
            Especies = especies,
        };

        var usuarioId = await _currentUserService.GetUsuarioActualIdAsync();
        entidad.HistorialDePrecios.Add(new HistorialPrecio
        {
            Monto = producto.PrecioActual,
            VigenteDesde = DateTime.UtcNow,
            ModificadoPorUsuarioId = usuarioId,
        });

        _dbContext.Productos.Add(entidad);
        await _dbContext.SaveChangesAsync();

        producto.Id = entidad.Id;
        return producto;
    }

    public async Task ActualizarAsync(ProductoDto producto)
    {
        var entidad = await _dbContext.Productos
            .Include(p => p.Especies)
            .Include(p => p.HistorialDePrecios)
            .FirstOrDefaultAsync(p => p.Id == producto.Id);
        if (entidad is null)
        {
            return;
        }

        entidad.Nombre = producto.Nombre;
        entidad.Descripcion = producto.Descripcion;
        entidad.UnidadDeMedida = producto.UnidadDeMedida;
        entidad.StockActual = producto.StockActual;
        entidad.Activo = producto.Activo;
        entidad.AlicuotaIva = producto.AlicuotaIva;
        entidad.CodigoDeBarras = string.IsNullOrWhiteSpace(producto.CodigoDeBarras) ? null : producto.CodigoDeBarras;
        entidad.CategoriaId = producto.CategoriaId;

        var especies = await _dbContext.Especies
            .Where(e => producto.EspecieIds.Contains(e.Id))
            .ToListAsync();
        entidad.Especies.Clear();
        foreach (var especie in especies)
        {
            entidad.Especies.Add(especie);
        }

        var precioVigente = entidad.HistorialDePrecios
            .OrderByDescending(h => h.VigenteDesde)
            .Select(h => h.Monto)
            .FirstOrDefault();

        if (precioVigente != producto.PrecioActual)
        {
            var usuarioId = await _currentUserService.GetUsuarioActualIdAsync();
            entidad.HistorialDePrecios.Add(new HistorialPrecio
            {
                Monto = producto.PrecioActual,
                VigenteDesde = DateTime.UtcNow,
                ModificadoPorUsuarioId = usuarioId,
            });
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var entidad = await _dbContext.Productos.FindAsync(id);
        if (entidad is null)
        {
            return true;
        }

        var tienePedidos = await _dbContext.ItemsPedido.AnyAsync(i => i.ProductoId == id);
        if (tienePedidos)
        {
            return false;
        }

        // ProductoSugerido no puede cascadear en SQL Server (las dos FK apuntan a Productos),
        // asi que se limpia a mano antes de borrar el producto.
        await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"DELETE FROM ProductoSugerido WHERE ProductoId = {id} OR ProductoSugeridoId = {id}");

        _dbContext.Productos.Remove(entidad);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<string> GenerarCodigoInternoAsync()
    {
        var existentes = await _dbContext.Productos
            .Where(p => p.CodigoDeBarras != null && p.CodigoDeBarras.StartsWith(PrefijoInterno) && p.CodigoDeBarras.Length == 13)
            .Select(p => p.CodigoDeBarras!)
            .ToListAsync();

        var siguiente = existentes
            .Select(c => long.TryParse(c.Substring(2, 10), out var numero) ? numero : 0)
            .DefaultIfEmpty(0)
            .Max() + 1;

        var doceDigitos = PrefijoInterno + siguiente.ToString("D10");
        return doceDigitos + CalcularDigitoVerificadorEan13(doceDigitos);
    }

    public async Task<ImagenProductoDto> AgregarImagenAsync(int productoId, Stream contenido, string nombreArchivo)
    {
        var ruta = await _archivoAlmacenamiento.GuardarAsync(contenido, nombreArchivo);

        var ordenSiguiente = await _dbContext.ImagenesProducto
            .Where(i => i.ProductoId == productoId)
            .Select(i => (int?)i.Orden)
            .MaxAsync() ?? -1;

        var imagen = new ImagenProducto
        {
            ProductoId = productoId,
            RutaArchivo = ruta,
            Orden = ordenSiguiente + 1,
        };

        _dbContext.ImagenesProducto.Add(imagen);
        await _dbContext.SaveChangesAsync();

        return new ImagenProductoDto { Id = imagen.Id, RutaArchivo = imagen.RutaArchivo, Orden = imagen.Orden };
    }

    public async Task EliminarImagenAsync(int imagenId)
    {
        var imagen = await _dbContext.ImagenesProducto.FindAsync(imagenId);
        if (imagen is null)
        {
            return;
        }

        _archivoAlmacenamiento.Eliminar(imagen.RutaArchivo);
        _dbContext.ImagenesProducto.Remove(imagen);
        await _dbContext.SaveChangesAsync();
    }

    private const string PrefijoInterno = "20";

    private static int CalcularDigitoVerificadorEan13(string doceDigitos)
    {
        var suma = 0;
        for (var i = 0; i < 12; i++)
        {
            var digito = doceDigitos[i] - '0';
            suma += digito * (i % 2 == 0 ? 1 : 3);
        }

        return (10 - (suma % 10)) % 10;
    }
}
