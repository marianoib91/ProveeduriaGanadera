namespace ProveeduriaGanadera.Application.Categorias;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> ListarAsync();
    Task<CategoriaDto> CrearAsync(CategoriaDto categoria);
    Task ActualizarAsync(CategoriaDto categoria);

    // false cuando no se puede eliminar porque hay productos usando esta categoria
    Task<bool> EliminarAsync(int id);
}
