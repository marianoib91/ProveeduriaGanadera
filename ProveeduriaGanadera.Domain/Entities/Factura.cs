using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Factura : EntityBase
{
    public TipoComprobante TipoComprobante { get; set; }
    public int PuntoDeVenta { get; set; }
    public int Numero { get; set; }
    public string Cae { get; set; } = string.Empty;
    public DateTime CaeVencimiento { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal MontoNeto { get; set; }
    public decimal MontoIva { get; set; }

    public string ClienteRazonSocialSnapshot { get; set; } = string.Empty;
    public string ClienteCuitSnapshot { get; set; } = string.Empty;
    public CondicionIva ClienteCondicionIvaSnapshot { get; set; }

    public int? PedidoId { get; set; }
    public Pedido? Pedido { get; set; }

    public int? DevolucionId { get; set; }
    public Devolucion? Devolucion { get; set; }

    public int? FacturaOriginalId { get; set; }
    public Factura? FacturaOriginal { get; set; }
}
