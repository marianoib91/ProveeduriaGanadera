using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Pedido : EntityBase
{
    public CanalPedido Canal { get; set; }
    public EstadoPedido Estado { get; set; }
    public DateTime? FechaRetiro { get; set; }
    public DateTime Fecha { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int? FranjaHorariaId { get; set; }
    public FranjaHoraria? FranjaHoraria { get; set; }

    public ICollection<ItemPedido> Items { get; set; } = new List<ItemPedido>();
    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    public Factura? Factura { get; set; }
    public ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();
    public MovimientoCuenta? MovimientoCuenta { get; set; }
    public ICollection<HistorialEstadoPedido> HistorialDeEstados { get; set; } = new List<HistorialEstadoPedido>();
}
