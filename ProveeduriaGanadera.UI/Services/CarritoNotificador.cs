namespace ProveeduriaGanadera.UI.Services;

// Servicio scoped (uno por circuito de Blazor Server) para que el badge del
// carrito en NavMenu se actualice al instante cuando Catalogo o Carrito
// modifican el contenido, sin necesidad de recargar la pagina.
public class CarritoNotificador
{
    public event Action? CambioCarrito;

    public void Notificar() => CambioCarrito?.Invoke();
}
