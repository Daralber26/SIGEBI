using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.Abstractions;

public interface IPenalizacionRepository
{
    Task<List<Penalizacion>> GetActivasPorUsuario(Guid usuarioId, CancellationToken ct);
}