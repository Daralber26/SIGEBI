using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.Abstractions;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct);
    Task<Usuario?> ObtenerPorIdAsync(Guid id, CancellationToken ct);
}