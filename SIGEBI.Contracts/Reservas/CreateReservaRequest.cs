namespace SIGEBI.Contracts.Reservas;

public class CreateReservaRequest
{
    public Guid UsuarioId { get; set; }
    public Guid RecursoId { get; set; }
}