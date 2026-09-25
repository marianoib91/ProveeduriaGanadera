namespace ProveeduriaGanadera.Application.Carrito;

public class CarritoDto
{
    public int PedidoId { get; set; }
    public List<ItemCarritoDto> Items { get; set; } = new();
    public int CantidadTotalItems => Items.Sum(i => i.Cantidad);
    public decimal Total => Items.Sum(i => i.Subtotal);
}
