using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.Abstractions;

public interface IPrestamoRepository
{
    Task AgregarAsync(Prestamo prestamo, CancellationToken ct);
    Task GuardarCambiosAsync(CancellationToken ct);

    Task<bool> ExistePrestamoActivoAsync(Guid recursoId, CancellationToken ct);
}