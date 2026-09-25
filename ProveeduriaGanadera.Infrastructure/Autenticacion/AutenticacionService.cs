using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProveeduriaGanadera.Application.Autenticacion;
using ProveeduriaGanadera.Domain.Entities;
using ProveeduriaGanadera.Domain.Enums;
using ProveeduriaGanadera.Infrastructure.Persistence;

namespace ProveeduriaGanadera.Infrastructure.Autenticacion;

public class AutenticacionService : IAutenticacionService
{
    private readonly ProveeduriaGanaderaDbContext _dbContext;
    private readonly PasswordHasher<Usuario> _hasher = new();

    public AutenticacionService(ProveeduriaGanaderaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UsuarioAutenticadoDto?> ValidarCredencialesAsync(string email, string password)
    {
        var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario is null || string.IsNullOrEmpty(usuario.PasswordHash))
        {
            return null;
        }

        var resultado = _hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, password);
        if (resultado == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return Mapear(usuario);
    }

    public async Task<(bool Exito, string? Error, UsuarioAutenticadoDto? Usuario)> RegistrarClienteAsync(RegistroDto registro)
    {
        var yaExiste = await _dbContext.Usuarios.AnyAsync(u => u.Email == registro.Email);
        if (yaExiste)
        {
            return (false, "Ya existe una cuenta con ese email.", null);
        }

        var usuario = new Usuario
        {
            Nombre = registro.Nombre,
            Apellido = registro.Apellido,
            Email = registro.Email,
            Rol = RolUsuario.Cliente,
        };
        usuario.PasswordHash = _hasher.HashPassword(usuario, registro.Password);

        var cliente = new Cliente
        {
            RazonSocialONombre = $"{registro.Nombre} {registro.Apellido}".Trim(),
            Cuit = string.Empty,
            CondicionIva = CondicionIva.ConsumidorFinal,
            DomicilioFiscal = string.Empty,
            Usuarios = new List<Usuario> { usuario },
        };

        _dbContext.Clientes.Add(cliente);
        await _dbContext.SaveChangesAsync();

        return (true, null, Mapear(usuario));
    }

    private static UsuarioAutenticadoDto Mapear(Usuario usuario) => new()
    {
        Id = usuario.Id,
        Nombre = usuario.Nombre,
        Apellido = usuario.Apellido,
        Email = usuario.Email,
        Rol = usuario.Rol.ToString(),
    };
}
