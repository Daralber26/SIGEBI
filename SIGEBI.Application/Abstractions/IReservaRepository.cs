using SIGEBI.Domain.Entities.Dbo;

namespace SIGEBI.Application.Abstractions;

public interface IReservaRepository
{
    Task<bool> ExistsActivaAsync(Guid usuarioId, Guid ejemplarId, CancellationToken ct = default);

    Task<bool> ExistsActivaByEjemplarAsync(Guid ejemplarId, CancellationToken ct = default);

    //   1 reserva activa por usuario y por recurso
    Task<bool> ExistsActivaByUsuarioYRecursoAsync(Guid usuarioId, Guid recursoId, CancellationToken ct = default);

    Task AddAsync(Reserva reserva, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}