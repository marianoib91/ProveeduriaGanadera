using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class ImagenProducto : EntityBase
{
    public string RutaArchivo { get; set; } = string.Empty;
    public int Orden { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
}
