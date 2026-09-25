namespace ProveeduriaGanadera.Application.Common;

// Abstrae DONDE se guardan los archivos subidos (hoy: wwwroot del host Web).
// El dia que exista Mobile, ese host implementa esta misma interfaz a su manera
// (almacenamiento local del dispositivo, o un storage remoto) sin tocar Application/Infrastructure.
public interface IArchivoAlmacenamientoService
{
    Task<string> GuardarAsync(Stream contenido, string nombreArchivo);

    void Eliminar(string rutaArchivo);
}
