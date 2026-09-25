using ProveeduriaGanadera.Domain.Common;

namespace ProveeduriaGanadera.Domain.Entities;

public class Especie : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string TerminoColoquial { get; set; } = string.Empty;

    public ICollection<Plantel> Planteles { get; set; } = new List<Plantel>();
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    public ICollection<ReglaCampania> Reglas { get; set; } = new List<ReglaCampania>();
}
