using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Recursos;

namespace SIGEBI.Application.Interfaces;

public interface IRecursoService : IBaseService
{
    Task<IReadOnlyList<RecursoDto>> GetAllActiveAsync(CancellationToken ct = default);

    Task<RecursoDto?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task AddAsync(SaveRecursoDto dto, CancellationToken ct = default);

    Task UpdateAsync(UpdateRecursoDto dto, CancellationToken ct = default);

    Task SoftDeleteAsync(RemoveRecursoDto dto, CancellationToken ct = default);
}