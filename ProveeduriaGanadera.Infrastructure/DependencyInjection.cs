using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProveeduriaGanadera.Application.Autenticacion;
using ProveeduriaGanadera.Application.Carrito;
using ProveeduriaGanadera.Application.Categorias;
using ProveeduriaGanadera.Application.Common;
using ProveeduriaGanadera.Application.Contacto;
using ProveeduriaGanadera.Application.Especies;
using ProveeduriaGanadera.Application.Productos;
using ProveeduriaGanadera.Infrastructure.Autenticacion;
using ProveeduriaGanadera.Infrastructure.Carrito;
using ProveeduriaGanadera.Infrastructure.Categorias;
using ProveeduriaGanadera.Infrastructure.Common;
using ProveeduriaGanadera.Infrastructure.Contacto;
using ProveeduriaGanadera.Infrastructure.Especies;
using ProveeduriaGanadera.Infrastructure.Persistence;
using ProveeduriaGanadera.Infrastructure.Productos;

namespace ProveeduriaGanadera.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Se registra via AddDbContextFactory (en vez del AddDbContext de siempre) para que
        // CarritoService pueda pedir instancias propias con IDbContextFactory: es el unico
        // servicio al que llama NavMenu (siempre interactivo) en paralelo a lo que este
        // haciendo la pagina activa, y compartir el DbContext scoped del circuito entre los
        // dos hace que EF Core tire "a second operation was started on this context instance".
        // El resto de los servicios sigue inyectando ProveeduriaGanaderaDbContext scoped tal
        // cual antes, sin tocarlos - el AddScoped de abajo arma esa instancia via el factory.
        services.AddDbContextFactory<ProveeduriaGanaderaDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("ProveeduriaGanaderaDb"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

        services.AddScoped(sp =>
            sp.GetRequiredService<IDbContextFactory<ProveeduriaGanaderaDbContext>>().CreateDbContext());

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IEspecieService, EspecieService>();
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<IAutenticacionService, AutenticacionService>();
        services.AddScoped<ICarritoService, CarritoService>();
        services.AddScoped<IContactoService, ContactoService>();

        return services;
    }
}
