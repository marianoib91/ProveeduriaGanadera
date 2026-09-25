using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class MovimientoCuenta : EntityBase
{
    public TipoMovimientoCuenta Tipo { get; set; }
    public decimal Monto { get; set; }
    public EstadoAprobacion EstadoAprobacion { get; set; }
    public DateTime Fecha { get; set; }

    public int CuentaCorrienteId { get; set; }
    public CuentaCorriente CuentaCorriente { get; set; } = null!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int? PedidoId { get; set; }
    public Pedido? Pedido { get; set; }

    public int? PagoId { get; set; }
    public Pago? Pago { get; set; }

    public int? DevolucionId { get; set; }
    public Devolucion? Devolucion { get; set; }
}
