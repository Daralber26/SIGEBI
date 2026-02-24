using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities;

public class Penalizacion
{
    public int Id { get; private set; }

    public Guid UsuarioId { get; private set; }
    public Guid PrestamoId { get; private set; }

    public DateTime FechaCreacionUtc { get; private set; }
    public DateTime? FechaFinUtc { get; private set; }

    public decimal? Monto { get; private set; }

    public PenalizacionEstado Estado { get; private set; }
    public PenalizacionMotivo Motivo { get; private set; }

    private Penalizacion() { } // EF

    public Penalizacion(
        Guid usuarioId,
        Guid prestamoId,
        PenalizacionMotivo motivo,
        decimal? monto = null,
        DateTime? fechaFinUtc = null)
    {
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("Usuario inválido.");

        if (prestamoId == Guid.Empty)
            throw new ArgumentException("Préstamo inválido.");

        // Regla: si es daño o pérdida, monto obligatorio
        if ((motivo == PenalizacionMotivo.Danio || motivo == PenalizacionMotivo.Perdida)
            && (monto == null || monto <= 0))
            throw new ArgumentException("El monto es obligatorio para daño o pérdida.");

        // Regla A: Retraso debe tener FechaFinUtc (por días)
        if (motivo == PenalizacionMotivo.Retraso && fechaFinUtc is null)
            throw new ArgumentException("FechaFinUtc es obligatoria para penalización por retraso.");

        UsuarioId = usuarioId;
        PrestamoId = prestamoId;
        Motivo = motivo;
        Monto = monto;
        FechaCreacionUtc = DateTime.UtcNow;
        FechaFinUtc = fechaFinUtc;
        Estado = PenalizacionEstado.Activa;
    }

    public void Resolver()
    {
        if (Estado != PenalizacionEstado.Activa)
            throw new InvalidOperationException("Solo penalizaciones activas pueden resolverse.");

        Estado = PenalizacionEstado.Resuelta;
    }

    public void Anular()
    {
        if (Estado != PenalizacionEstado.Activa)
            throw new InvalidOperationException("Solo penalizaciones activas pueden anularse.");

        Estado = PenalizacionEstado.Anulada;
    }

    public bool EstaActiva() => Estado == PenalizacionEstado.Activa;

    public bool Bloquea(DateTime utcNow)
    {
        if (Estado != PenalizacionEstado.Activa)
            return false;

        // Si es por retraso y tiene fecha fin: bloquea hasta que llegue la fecha
        if (Motivo == PenalizacionMotivo.Retraso)
            return FechaFinUtc is null || FechaFinUtc > utcNow;

        // Daño o pérdida: bloquea mientras esté activa (hasta que la resuelvan/anulen)
        return true;
    }


}