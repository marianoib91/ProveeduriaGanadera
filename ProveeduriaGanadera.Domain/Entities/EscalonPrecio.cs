using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class EscalonPrecio : EntityBase
{
    public int CantidadMinima { get; set; }
    public decimal PrecioUnitario { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
}
