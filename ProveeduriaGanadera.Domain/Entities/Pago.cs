using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Pago : EntityBase
{
    public MedioDePago MedioDePago { get; set; }
    public decimal Monto { get; set; }
    public PagoEstado Estado { get; set; }
    public SentidoPago Sentido { get; set; }

    public int? PedidoId { get; set; }
    public Pedido? Pedido { get; set; }

    public int? CajaId { get; set; }
    public Caja? Caja { get; set; }

    public int? DevolucionId { get; set; }
    public Devolucion? Devolucion { get; set; }

    public MovimientoCuenta? MovimientoCuenta { get; set; }
}
