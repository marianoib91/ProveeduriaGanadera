using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Devolucion : EntityBase
{
    public EstadoDevolucion Estado { get; set; }
    public DateTime Fecha { get; set; }
    public FormaReintegro FormaReintegro { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int MotivoDevolucionId { get; set; }
    public MotivoDevolucion MotivoDevolucion { get; set; } = null!;

    public ICollection<DetalleDevolucion> Detalles { get; set; } = new List<DetalleDevolucion>();

    public Factura? NotaDeCredito { get; set; }
    public MovimientoCuenta? MovimientoCuentaGenerado { get; set; }
    public Pago? PagoGenerado { get; set; }
}
