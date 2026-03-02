namespace SIGEBI.Domain.Entities.Dbo;

public class Reserva
{
    public Guid Id { get; private set; }

    public Guid UsuarioId { get; private set; }
    public Guid EjemplarId { get; private set; }  // ✅ ahora reserva un ejemplar real

    public DateTime FechaCreacionUtc { get; private set; }
    public DateTime? FechaCancelacionUtc { get; private set; }

    private Reserva() { } // EF

    public Reserva(Guid usuarioId, Guid ejemplarId)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("UsuarioId inválido.");
        if (ejemplarId == Guid.Empty) throw new ArgumentException("EjemplarId inválido.");

        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        EjemplarId = ejemplarId;
        FechaCreacionUtc = DateTime.UtcNow;
        FechaCancelacionUtc = null;
    }

    public void Cancelar()
    {
        if (FechaCancelacionUtc is not null) return;
        FechaCancelacionUtc = DateTime.UtcNow;
    }
}