using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Resources;

namespace SIGEBI.Application.UseCases.Catalogo;

public class ListarCatalogo
{
    private readonly ICatalogoRepository _repo;

    public ListarCatalogo(ICatalogoRepository repo)
    {
        _repo = repo;
    }

    public Task<List<ResourceDto>> ExecuteAsync(CancellationToken ct)
    {
        return _repo.ListarCatalogoAsync(ct);
    }
}