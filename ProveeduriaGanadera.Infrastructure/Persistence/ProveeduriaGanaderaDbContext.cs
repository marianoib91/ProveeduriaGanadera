using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProveeduriaGanadera.Domain.Entities;

namespace ProveeduriaGanadera.Infrastructure.Persistence;

public class ProveeduriaGanaderaDbContext : DbContext
{
    public ProveeduriaGanaderaDbContext(DbContextOptions<ProveeduriaGanaderaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Configuracion> Configuraciones => Set<Configuracion>();
    public DbSet<Especie> Especies => Set<Especie>();
    public DbSet<Plantel> Planteles => Set<Plantel>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<HistorialPrecio> HistorialesDePrecio => Set<HistorialPrecio>();
    public DbSet<EscalonPrecio> EscalonesDePrecio => Set<EscalonPrecio>();
    public DbSet<Lote> Lotes => Set<Lote>();
    public DbSet<ImagenProducto> ImagenesProducto => Set<ImagenProducto>();
    public DbSet<FranjaHoraria> FranjasHorarias => Set<FranjaHoraria>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<HistorialEstadoPedido> HistorialesDeEstadoPedido => Set<HistorialEstadoPedido>();
    public DbSet<ItemPedido> ItemsPedido => Set<ItemPedido>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<Caja> Cajas => Set<Caja>();
    public DbSet<CuentaCorriente> CuentasCorrientes => Set<CuentaCorriente>();
    public DbSet<MovimientoCuenta> MovimientosCuenta => Set<MovimientoCuenta>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<Devolucion> Devoluciones => Set<Devolucion>();
    public DbSet<MotivoDevolucion> MotivosDevolucion => Set<MotivoDevolucion>();
    public DbSet<DetalleDevolucion> DetallesDevolucion => Set<DetalleDevolucion>();
    public DbSet<ReglaCampania> ReglasCampania => Set<ReglaCampania>();
    public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
    public DbSet<MensajeContacto> MensajesContacto => Set<MensajeContacto>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Clientes)
            .WithMany(c => c.Usuarios)
            .UsingEntity(j => j.ToTable("UsuarioCliente"));

        modelBuilder.Entity<Producto>()
            .HasMany(p => p.Especies)
            .WithMany(e => e.Productos)
            .UsingEntity(j => j.ToTable("ProductoEspecie"));

        modelBuilder.Entity<Producto>()
            .HasMany(p => p.ProductosSugeridos)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductoSugerido",
                j => j.HasOne<Producto>().WithMany().HasForeignKey("ProductoSugeridoId"),
                j => j.HasOne<Producto>().WithMany().HasForeignKey("ProductoId"));

        // El nombre de estas dos propiedades no sigue la convencion de EF (<Navegacion>Id),
        // asi que sin esto EF crea una FK sombra separada (ModificadoPorId/ActualizadoPorId)
        // que nunca se completa y deja la de verdad en 0 -> viola la constraint.
        modelBuilder.Entity<HistorialPrecio>()
            .HasOne(h => h.ModificadoPor)
            .WithMany(u => u.CambiosDePrecio)
            .HasForeignKey(h => h.ModificadoPorUsuarioId);

        modelBuilder.Entity<Configuracion>()
            .HasOne(c => c.ActualizadoPor)
            .WithMany(u => u.ConfiguracionesActualizadas)
            .HasForeignKey(c => c.ActualizadoPorUsuarioId);

        // Por defecto ninguna relacion borra en cascada: en este dominio casi ninguna
        // baja deberia arrastrar registros historicos/fiscales. Se habilita cascada
        // solo donde el detalle no tiene sentido sin su padre (lineas de pedido/devolucion),
        // y en las tablas intermedias de muchos-a-muchos (una fila ahi no significa nada por
        // si sola, sin cascada quedan indestructibles desde ninguno de los dos lados).
        // ProductoSugerido queda afuera a proposito: al ser una relacion Producto-Producto
        // (las dos FK apuntan a la misma tabla), SQL Server rechaza la cascada por "multiple
        // cascade paths" -- esa limpieza se hace a mano en ProductoService antes de borrar.
        var tablasIntermedias = new[] { "UsuarioCliente", "ProductoEspecie" };
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            var tabla = foreignKey.DeclaringEntityType.GetTableName();
            foreignKey.DeleteBehavior = tablasIntermedias.Contains(tabla)
                ? DeleteBehavior.Cascade
                : DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<ItemPedido>()
            .HasOne(i => i.Pedido)
            .WithMany(p => p.Items)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DetalleDevolucion>()
            .HasOne(d => d.Devolucion)
            .WithMany(d => d.Detalles)
            .OnDelete(DeleteBehavior.Cascade);

        // Mismo criterio: HistorialPrecio, EscalonPrecio y Lote son datos propios del
        // producto, sin significado fuera de el, asi que se van con el producto.
        modelBuilder.Entity<HistorialPrecio>()
            .HasOne(h => h.Producto)
            .WithMany(p => p.HistorialDePrecios)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EscalonPrecio>()
            .HasOne(e => e.Producto)
            .WithMany(p => p.EscalonesDePrecio)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lote>()
            .HasOne(l => l.Producto)
            .WithMany(p => p.Lotes)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ImagenProducto>()
            .HasOne(i => i.Producto)
            .WithMany(p => p.Imagenes)
            .OnDelete(DeleteBehavior.Cascade);

        // Unico cuando esta cargado, pero SQL Server permite varios NULL en un indice
        // unico (cada NULL cuenta distinto), asi que los productos sin codigo no chocan.
        modelBuilder.Entity<Producto>()
            .HasIndex(p => p.CodigoDeBarras)
            .IsUnique();

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
