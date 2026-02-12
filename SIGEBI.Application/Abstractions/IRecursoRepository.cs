using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.Abstractions;

public interface IRecursoRepository
{
    Task<List<Recurso>> ListarAsync(CancellationToken ct);

    Task<Recurso?> ObtenerPorIdAsync(Guid id, CancellationToken ct);

    Task AgregarAsync(Recurso recurso, CancellationToken ct);

    Task EliminarAsync(Recurso recurso, CancellationToken ct);

    Task GuardarCambiosAsync(CancellationToken ct);
}
