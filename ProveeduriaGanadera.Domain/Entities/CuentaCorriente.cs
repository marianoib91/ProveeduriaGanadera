using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class CuentaCorriente : EntityBase
{
    public decimal Saldo { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public ICollection<MovimientoCuenta> Movimientos { get; set; } = new List<MovimientoCuenta>();
}
