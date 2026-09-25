namespace ProveeduriaGanadera.Application.Productos;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string UnidadDeMedida { get; set; } = string.Empty;
    public int StockActual { get; set; }
    public bool Activo { get; set; } = true;
    public decimal AlicuotaIva { get; set; }
    public decimal PrecioActual { get; set; }
    public string? CodigoDeBarras { get; set; }
    public List<ImagenProductoDto> Imagenes { get; set; } = new();

    public int CategoriaId { get; set; }
    public string CategoriaNombre { get; set; } = string.Empty;

    public List<int> EspecieIds { get; set; } = new();
    public List<string> EspecieNombres { get; set; } = new();
}
