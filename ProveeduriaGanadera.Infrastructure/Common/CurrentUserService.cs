using Microsoft.EntityFrameworkCore;
using ProveeduriaGanadera.Application.Common;
using ProveeduriaGanadera.Domain.Entities;
using ProveeduriaGanadera.Domain.Enums;
using ProveeduriaGanadera.Infrastructure.Persistence;

namespace ProveeduriaGanadera.Infrastructure.Common;

public class CurrentUserService : ICurrentUserService
{
    private readonly ProveeduriaGanaderaDbContext _dbContext;

    public CurrentUserService(ProveeduriaGanaderaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetUsuarioActualIdAsync()
    {
        var admin = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Rol == RolUsuario.Admin);
        if (admin is not null)
        {
            return admin.Id;
        }

        var nuevo = new Usuario
        {
            Nombre = "Admin",
            Apellido = "Sistema",
            Email = "admin@proveeduriaganadera.local",
            PasswordHash = string.Empty,
            Rol = RolUsuario.Admin,
        };
        _dbContext.Usuarios.Add(nuevo);
        await _dbContext.SaveChangesAsync();
        return nuevo.Id;
    }
}
