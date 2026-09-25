namespace ProveeduriaGanadera.Application.Autenticacion;

public interface IAutenticacionService
{
    Task<UsuarioAutenticadoDto?> ValidarCredencialesAsync(string email, string password);

    // Crea el Usuario (rol Cliente) y su propia cuenta Cliente asociada, lista para comprar a nombre propio.
    Task<(bool Exito, string? Error, UsuarioAutenticadoDto? Usuario)> RegistrarClienteAsync(RegistroDto registro);
}
