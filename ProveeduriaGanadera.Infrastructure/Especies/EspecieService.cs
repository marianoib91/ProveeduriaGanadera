using Microsoft.EntityFrameworkCore;
using ProveeduriaGanadera.Application.Especies;
using ProveeduriaGanadera.Domain.Entities;
using ProveeduriaGanadera.Infrastructure.Persistence;

namespace ProveeduriaGanadera.Infrastructure.Especies;

public class EspecieService : IEspecieService
{
    private readonly ProveeduriaGanaderaDbContext _dbContext;

    public EspecieService(ProveeduriaGanaderaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<EspecieDto>> ListarAsync()
    {
        return await _dbContext.Especies
            .OrderBy(e => e.Nombre)
            .Select(e => new EspecieDto { Id = e.Id, Nombre = e.Nombre, TerminoColoquial = e.TerminoColoquial })
            .ToListAsync();
    }

    public async Task<EspecieDto> CrearAsync(EspecieDto especie)
    {
        var entidad = new Especie { Nombre = especie.Nombre, TerminoColoquial = especie.TerminoColoquial };
        _dbContext.Especies.Add(entidad);
        await _dbContext.SaveChangesAsync();

        especie.Id = entidad.Id;
        return especie;
    }

    public async Task ActualizarAsync(EspecieDto especie)
    {
        var entidad = await _dbContext.Especies.FindAsync(especie.Id);
        if (entidad is null)
        {
            return;
        }

        entidad.Nombre = especie.Nombre;
        entidad.TerminoColoquial = especie.TerminoColoquial;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var entidad = await _dbContext.Especies.FindAsync(id);
        if (entidad is null)
        {
            return true;
        }

        var enUso = await _dbContext.Planteles.AnyAsync(p => p.EspecieId == id)
            || await _dbContext.Productos.AnyAsync(p => p.Especies.Any(e => e.Id == id));
        if (enUso)
        {
            return false;
        }

        _dbContext.Especies.Remove(entidad);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
