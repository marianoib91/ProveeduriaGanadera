using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class HistorialEstadoPedido : EntityBase
{
    public EstadoPedido EstadoAnterior { get; set; }
    public EstadoPedido EstadoNuevo { get; set; }
    public DateTime Fecha { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
