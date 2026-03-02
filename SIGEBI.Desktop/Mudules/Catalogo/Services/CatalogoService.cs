using SIGEBI.Contracts.Resources;
using SIGEBI.Desktop.Modules.Catalogo.Interfaces;
using SIGEBI.Desktop.Shared;

namespace SIGEBI.Desktop.Modules.Catalogo.Services;

public sealed class CatalogoService : ICatalogoService
{
    private readonly ApiClient _api;

    public CatalogoService(ApiClient api)
    {
        _api = api;
    }

    public async Task<IReadOnlyList<ResourceDto>> ListarAsync(CancellationToken ct = default)
    {
        return await _api.GetAsync<List<ResourceDto>>("/catalogo", ct);
    }

}