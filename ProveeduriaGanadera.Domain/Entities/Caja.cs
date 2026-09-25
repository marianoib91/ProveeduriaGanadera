using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class Caja : EntityBase
{
    public DateTime FechaApertura { get; set; }
    public decimal FondoInicial { get; set; }
    public DateTime? FechaCierre { get; set; }
    public decimal? EfectivoContado { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
