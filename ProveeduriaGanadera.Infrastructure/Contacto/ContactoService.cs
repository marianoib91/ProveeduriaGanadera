using ProveeduriaGanadera.Application.Contacto;
using ProveeduriaGanadera.Domain.Entities;
using ProveeduriaGanadera.Infrastructure.Persistence;

namespace ProveeduriaGanadera.Infrastructure.Contacto;

public class ContactoService : IContactoService
{
    private readonly ProveeduriaGanaderaDbContext _dbContext;

    public ContactoService(ProveeduriaGanaderaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnviarMensajeAsync(MensajeContactoDto mensaje)
    {
        _dbContext.MensajesContacto.Add(new MensajeContacto
        {
            Nombre = mensaje.Nombre,
            Email = mensaje.Email,
            Mensaje = mensaje.Mensaje,
            Fecha = DateTime.UtcNow,
        });

        await _dbContext.SaveChangesAsync();
    }
}
