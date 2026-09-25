using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class Configuracion : EntityBase
{
    public decimal LimiteCreditoPorDefecto { get; set; }

    public int ActualizadoPorUsuarioId { get; set; }
    public Usuario ActualizadoPor { get; set; } = null!;
}
