using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.Abstractions;

public interface IPrestamoRepository
{
    Task AgregarAsync(Prestamo prestamo, CancellationToken ct);
    Task GuardarCambiosAsync(CancellationToken ct);

    Task<bool> ExistePrestamoActivoAsync(Guid ejemplarId, CancellationToken ct);

    Task<Prestamo?> ObtenerPorIdAsync(Guid id, CancellationToken ct);

    Task<int> ContarPrestamosActivosPorUsuarioAsync(Guid usuarioId, CancellationToken ct);
}
