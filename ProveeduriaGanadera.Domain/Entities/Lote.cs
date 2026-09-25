using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class Lote : EntityBase
{
    public DateTime Vencimiento { get; set; }
    public int Cantidad { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
}
