using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class MotivoDevolucion : EntityBase
{
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();
}
