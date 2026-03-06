using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities;

public class Notificacion
{
    public int Id { get; private set; }
    public Guid UsuarioId { get; private set; }

    public NotificacionTipo Tipo { get; private set; }
    public NotificacionEstado Estado { get; private set; }

    public string Mensaje { get; private set; } = string.Empty;

    public DateTime FechaCreacionUtc { get; private set; }
    public DateTime? FechaLecturaUtc { get; private set; }

    private Notificacion() { } // EF

    public Notificacion(Guid usuarioId, NotificacionTipo tipo, string mensaje)
    {
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("Usuario inválido.");

        if (string.IsNullOrWhiteSpace(mensaje))
            throw new ArgumentException("El mensaje es obligatorio.");

        mensaje = mensaje.Trim();

        if (mensaje.Length > 500)
            throw new ArgumentException("El mensaje no puede exceder 500 caracteres.");

        UsuarioId = usuarioId;
        Tipo = tipo;
        Mensaje = mensaje;

        FechaCreacionUtc = DateTime.UtcNow;
        Estado = NotificacionEstado.Pendiente;
    }

    public void MarcarLeida()
    {
        if (Estado == NotificacionEstado.Leida)
            return;

        Estado = NotificacionEstado.Leida;
        FechaLecturaUtc = DateTime.UtcNow;
    }
}