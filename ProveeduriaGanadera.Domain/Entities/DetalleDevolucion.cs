using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class DetalleDevolucion : EntityBase
{
    public int Cantidad { get; set; }

    public int DevolucionId { get; set; }
    public Devolucion Devolucion { get; set; } = null!;

    public int ItemPedidoId { get; set; }
    public ItemPedido ItemPedido { get; set; } = null!;
}
