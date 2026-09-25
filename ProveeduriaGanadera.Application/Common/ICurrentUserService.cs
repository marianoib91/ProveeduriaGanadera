namespace ProveeduriaGanadera.Application.Common;

// Punto unico de "quien esta haciendo esto" para los campos de auditoria.
// Hoy no hay login real, asi que la implementacion usa/crea un usuario admin
// de arranque; el dia que haya autenticacion, se reemplaza sin tocar los
// servicios que la consumen (HistorialPrecio, MovimientoCuenta, etc).
public interface ICurrentUserService
{
    Task<int> GetUsuarioActualIdAsync();
}
