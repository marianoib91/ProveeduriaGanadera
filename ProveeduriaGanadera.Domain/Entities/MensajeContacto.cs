using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class MensajeContacto : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
}
