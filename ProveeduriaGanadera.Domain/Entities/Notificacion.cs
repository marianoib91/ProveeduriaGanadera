using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Notificacion : EntityBase
{
    public CanalNotificacion Canal { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public DateTime? FechaEnvio { get; set; }
    public EstadoNotificacion Estado { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int? ReglaCampaniaId { get; set; }
    public ReglaCampania? ReglaCampania { get; set; }
}
