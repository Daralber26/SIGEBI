namespace SIGEBI.Contracts.Prestamos;

public record PrestamoResponse(
    Guid Id,
    Guid UsuarioId,
    Guid EjemplarId,
    DateTime FechaPrestamo,
    DateTime FechaVencimiento,
    DateTime? FechaDevolucion
);