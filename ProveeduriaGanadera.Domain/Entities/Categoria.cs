using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class Categoria : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    public ICollection<ReglaCampania> Reglas { get; set; } = new List<ReglaCampania>();
}
