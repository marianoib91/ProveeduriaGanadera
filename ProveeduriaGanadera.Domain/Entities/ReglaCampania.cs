using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class ReglaCampania : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string MensajePlantilla { get; set; } = string.Empty;
    public TipoDisparoCampania TipoDisparo { get; set; }
    public int? IntervaloDias { get; set; }
    public int? MesInicio { get; set; }
    public int? DiaInicio { get; set; }
    public bool Activa { get; set; } = true;

    public int? EspecieId { get; set; }
    public Especie? Especie { get; set; }

    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
}
