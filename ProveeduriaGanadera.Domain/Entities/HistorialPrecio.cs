using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class HistorialPrecio : EntityBase
{
    public decimal Monto { get; set; }
    public DateTime VigenteDesde { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;

    public int ModificadoPorUsuarioId { get; set; }
    public Usuario ModificadoPor { get; set; } = null!;
}
