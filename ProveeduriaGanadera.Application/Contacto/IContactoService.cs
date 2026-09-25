namespace ProveeduriaGanadera.Application.Contacto;

// Por ahora los mensajes solo quedan guardados en la base (no hay SMTP/proveedor de
// email configurado todavia). El dia que se defina una casilla real, esta es la
// unica implementacion que hay que tocar para que ademas se envie el email.
public interface IContactoService
{
    Task EnviarMensajeAsync(MensajeContactoDto mensaje);
}
