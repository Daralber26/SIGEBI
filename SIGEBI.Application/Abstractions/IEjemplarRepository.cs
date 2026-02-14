using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.Abstractions;

public interface IEjemplarRepository
{
    Task AgregarAsync(Ejemplar ejemplar, CancellationToken ct);
    Task GuardarCambiosAsync(CancellationToken ct);
    Task<bool> ExisteCodigoAsync(string codigo, CancellationToken ct);
    Task<Ejemplar?> ObtenerPorIdAsync(Guid id, CancellationToken ct);
}
