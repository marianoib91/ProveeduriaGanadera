namespace ProveeduriaGanadera.Application.Carrito;

// El carrito es un Pedido persistido en estado EstadoPedido.Carrito desde que se
// agrega el primer producto (decision explicita: sobrevive cierre de sesion y
// multi-dispositivo, a costa de un estado mas en la maquina de estados de Pedido).
// Pasa a Pendiente recien cuando el cliente confirma (pantalla todavia no implementada).
public interface ICarritoService
{
    Task<CarritoDto> ObtenerAsync(int usuarioId);
    Task<CarritoDto> AgregarAsync(int usuarioId, int productoId, int cantidad);
    Task<CarritoDto> ActualizarCantidadAsync(int usuarioId, int itemPedidoId, int cantidad);
    Task<CarritoDto> QuitarAsync(int usuarioId, int itemPedidoId);
    Task<int> ContarItemsAsync(int usuarioId);
}
