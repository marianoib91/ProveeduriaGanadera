using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Plantel : EntityBase
{
    public int CantidadCabezas { get; set; }
    public TipoProduccion TipoProduccion { get; set; }
    public DateTime ActualizadoEl { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int EspecieId { get; set; }
    public Especie Especie { get; set; } = null!;
}
