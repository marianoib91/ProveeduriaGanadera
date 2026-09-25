using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class ItemPedido : EntityBase
{
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;

    public ICollection<DetalleDevolucion> DetallesDevueltos { get; set; } = new List<DetalleDevolucion>();
}
