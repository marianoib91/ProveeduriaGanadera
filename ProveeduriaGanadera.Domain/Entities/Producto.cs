using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class Producto : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string UnidadDeMedida { get; set; } = string.Empty;
    public int StockActual { get; set; }
    public bool Activo { get; set; } = true;
    public decimal AlicuotaIva { get; set; }
    public string? CodigoDeBarras { get; set; }

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public ICollection<Especie> Especies { get; set; } = new List<Especie>();
    public ICollection<EscalonPrecio> EscalonesDePrecio { get; set; } = new List<EscalonPrecio>();
    public ICollection<HistorialPrecio> HistorialDePrecios { get; set; } = new List<HistorialPrecio>();
    public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    public ICollection<ItemPedido> ItemsDePedido { get; set; } = new List<ItemPedido>();
    public ICollection<ImagenProducto> Imagenes { get; set; } = new List<ImagenProducto>();

    public ICollection<Producto> ProductosSugeridos { get; set; } = new List<Producto>();
}
