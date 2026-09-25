using Microsoft.EntityFrameworkCore;
using ProveeduriaGanadera.Application.Categorias;
using ProveeduriaGanadera.Domain.Entities;
using ProveeduriaGanadera.Infrastructure.Persistence;

namespace ProveeduriaGanadera.Infrastructure.Categorias;

public class CategoriaService : ICategoriaService
{
    private readonly ProveeduriaGanaderaDbContext _dbContext;

    public CategoriaService(ProveeduriaGanaderaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CategoriaDto>> ListarAsync()
    {
        return await _dbContext.Categorias
            .OrderBy(c => c.Nombre)
            .Select(c => new CategoriaDto { Id = c.Id, Nombre = c.Nombre, Descripcion = c.Descripcion })
            .ToListAsync();
    }

    public async Task<CategoriaDto> CrearAsync(CategoriaDto categoria)
    {
        var entidad = new Categoria { Nombre = categoria.Nombre, Descripcion = categoria.Descripcion };
        _dbContext.Categorias.Add(entidad);
        await _dbContext.SaveChangesAsync();

        categoria.Id = entidad.Id;
        return categoria;
    }

    public async Task ActualizarAsync(CategoriaDto categoria)
    {
        var entidad = await _dbContext.Categorias.FindAsync(categoria.Id);
        if (entidad is null)
        {
            return;
        }

        entidad.Nombre = categoria.Nombre;
        entidad.Descripcion = categoria.Descripcion;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var entidad = await _dbContext.Categorias.FindAsync(id);
        if (entidad is null)
        {
            return true;
        }

        var tieneProductos = await _dbContext.Productos.AnyAsync(p => p.CategoriaId == id);
        if (tieneProductos)
        {
            return false;
        }

        _dbContext.Categorias.Remove(entidad);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
