using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Cliente : EntityBase
{
    public string RazonSocialONombre { get; set; } = string.Empty;
    public string Cuit { get; set; } = string.Empty;
    public CondicionIva CondicionIva { get; set; }
    public string DomicilioFiscal { get; set; } = string.Empty;
    public decimal? LimiteCredito { get; set; }
    public bool SinLimite { get; set; }

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    public CuentaCorriente? CuentaCorriente { get; set; }
    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    public ICollection<Plantel> Planteles { get; set; } = new List<Plantel>();
}
