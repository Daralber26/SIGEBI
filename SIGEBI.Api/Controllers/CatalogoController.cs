using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.UseCases.Catalogo;
using SIGEBI.Contracts.Resources;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("catalogo")]
public class CatalogoController : ControllerBase
{
    private readonly ListarCatalogo _uc;

    public CatalogoController(ListarCatalogo uc)
    {
        _uc = uc;
    }

    [HttpGet]
    public async Task<ActionResult<List<ResourceDto>>> Get(CancellationToken ct)
    {
        var result = await _uc.ExecuteAsync(ct);
        return Ok(result);
    }
}
