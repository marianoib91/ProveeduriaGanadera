using ProveeduriaGanadera.Application.Common;

namespace ProveeduriaGanadera.Web.Services;

public class ArchivoAlmacenamientoWebService : IArchivoAlmacenamientoService
{
    private const string CarpetaRelativa = "uploads/productos";
    private readonly IWebHostEnvironment _entorno;

    public ArchivoAlmacenamientoWebService(IWebHostEnvironment entorno)
    {
        _entorno = entorno;
    }

    public async Task<string> GuardarAsync(Stream contenido, string nombreArchivo)
    {
        var carpetaFisica = Path.Combine(_entorno.WebRootPath, CarpetaRelativa);
        Directory.CreateDirectory(carpetaFisica);

        var nombreUnico = $"{Guid.NewGuid()}{Path.GetExtension(nombreArchivo)}";
        var rutaFisica = Path.Combine(carpetaFisica, nombreUnico);

        await using var destino = File.Create(rutaFisica);
        await contenido.CopyToAsync(destino);

        return $"/{CarpetaRelativa}/{nombreUnico}";
    }

    public void Eliminar(string rutaArchivo)
    {
        var rutaFisica = Path.Combine(_entorno.WebRootPath, rutaArchivo.TrimStart('/'));
        if (File.Exists(rutaFisica))
        {
            File.Delete(rutaFisica);
        }
    }
}
