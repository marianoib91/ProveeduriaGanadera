namespace ProveeduriaGanadera.Domain.Enums;

public enum RolUsuario
{
    Admin,
    Empleado,
    Cliente
}

public enum CondicionIva
{
    ResponsableInscripto,
    Monotributista,
    ConsumidorFinal
}

public enum TipoProduccion
{
    Cria,
    Invernada,
    CicloCompleto
}

public enum CanalPedido
{
    EnLocal,
    Online
}

public enum EstadoPedido
{
    Carrito,
    Pendiente,
    Confirmado,
    Preparado,
    Retirado,
    Cancelado
}

public enum MedioDePago
{
    Efectivo,
    Transferencia,
    Online,
    Tarjeta
}

public enum PagoEstado
{
    Registrado,
    Anulado
}

public enum SentidoPago
{
    Ingreso,
    Egreso
}

public enum TipoMovimientoCuenta
{
    Cargo,
    Pago,
    Credito
}

public enum EstadoAprobacion
{
    Pendiente,
    Aprobado,
    Rechazado
}

public enum TipoComprobante
{
    A,
    B,
    C,
    NotaCreditoA,
    NotaCreditoB,
    NotaCreditoC
}

public enum EstadoDevolucion
{
    Pendiente,
    Aprobada,
    Rechazada
}

public enum FormaReintegro
{
    CuentaCorriente,
    Efectivo
}

public enum TipoDisparoCampania
{
    FechaFija,
    IntervaloDesdeUltimaCompra
}

public enum CanalNotificacion
{
    Push,
    Email,
    WhatsApp
}

public enum EstadoNotificacion
{
    Pendiente,
    Enviada,
    Fallida
}
