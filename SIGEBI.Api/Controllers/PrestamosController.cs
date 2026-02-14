using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.UseCases.Prestamos;
using SIGEBI.Contracts.Prestamos;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamosController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CreatePrestamoRequest request,
        [FromServices] CrearPrestamo crear,
        CancellationToken ct)
    {
        var prestamo = await crear.Ejecutar(request, ct);
        return Ok(prestamo);
    }
}
