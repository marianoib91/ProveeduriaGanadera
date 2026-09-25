namespace ProveeduriaGanadera.Application.Productos;

public class ImagenProductoDto
{
    public int Id { get; set; }
    public string RutaArchivo { get; set; } = string.Empty;
    public int Orden { get; set; }
}
