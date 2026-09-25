using ProveeduriaGanadera.Domain.Common;
using ProveeduriaGanadera.Domain.Enums;

namespace ProveeduriaGanadera.Domain.Entities;

public class Usuario : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RolUsuario Rol { get; set; }

    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    public ICollection<Pedido> PedidosRealizados { get; set; } = new List<Pedido>();
    public ICollection<HistorialPrecio> CambiosDePrecio { get; set; } = new List<HistorialPrecio>();
    public ICollection<Configuracion> ConfiguracionesActualizadas { get; set; } = new List<Configuracion>();
    public ICollection<MovimientoCuenta> MovimientosGestionados { get; set; } = new List<MovimientoCuenta>();
    public ICollection<Caja> CajasGestionadas { get; set; } = new List<Caja>();
    public ICollection<HistorialEstadoPedido> CambiosDeEstadoPedido { get; set; } = new List<HistorialEstadoPedido>();
    public ICollection<Devolucion> DevolucionesGestionadas { get; set; } = new List<Devolucion>();
    public ICollection<ReglaCampania> ReglasGestionadas { get; set; } = new List<ReglaCampania>();
}
