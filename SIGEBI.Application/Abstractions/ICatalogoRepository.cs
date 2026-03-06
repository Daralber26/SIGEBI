using SIGEBI.Contracts.Resources;

namespace SIGEBI.Application.Abstractions;

public interface ICatalogoRepository
{
    Task<List<ResourceDto>> ListarCatalogoAsync(CancellationToken ct);
}