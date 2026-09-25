namespace ProveeduriaGanadera.Application.Productos;

public interface IProductoService
{
    Task<List<ProductoDto>> ListarAsync();
    Task<ProductoDto?> ObtenerAsync(int id);
    Task<ProductoDto> CrearAsync(ProductoDto producto);
    Task ActualizarAsync(ProductoDto producto);

    // false cuando no se puede eliminar porque hay pedidos que ya lo referencian
    // (en ese caso conviene desactivarlo con Activo = false, no borrarlo)
    Task<bool> EliminarAsync(int id);

    // EAN-13 valido dentro del rango 20-29 reservado por GS1 para uso interno,
    // para productos que no traen codigo de fabrica.
    Task<string> GenerarCodigoInternoAsync();

    // El producto tiene que existir de antes (se sube al editar, no al dar de alta).
    Task<ImagenProductoDto> AgregarImagenAsync(int productoId, Stream contenido, string nombreArchivo);
    Task EliminarImagenAsync(int imagenId);
}
