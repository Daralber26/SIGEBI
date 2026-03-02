using SIGEBI.Contracts.Resources;

namespace SIGEBI.Desktop.Modules.Catalogo.Interfaces;

public interface ICatalogoService
{
    Task<IReadOnlyList<ResourceDto>> ListarAsync(CancellationToken ct = default);
}