namespace ProveeduriaGanadera.Application.Especies;

public interface IEspecieService
{
    Task<List<EspecieDto>> ListarAsync();
    Task<EspecieDto> CrearAsync(EspecieDto especie);
    Task ActualizarAsync(EspecieDto especie);

    // false cuando no se puede eliminar porque hay planteles o productos que la usan
    Task<bool> EliminarAsync(int id);
}
