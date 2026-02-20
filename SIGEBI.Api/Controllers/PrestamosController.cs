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

        return Ok(new PrestamoResponse(
            prestamo.Id,
            prestamo.UsuarioId,
            prestamo.EjemplarId,
            prestamo.FechaPrestamo,
            prestamo.FechaVencimiento,
            prestamo.FechaDevolucion
        ));
    }

    [HttpPut("{id:guid}/devolver")]
    public async Task<IActionResult> Devolver(
        [FromRoute] Guid id,
        [FromServices] DevolverPrestamo devolver,
        CancellationToken ct)
    {
        await devolver.EjecutarAsync(id, ct);

        return Ok(new
        {
            message = "Préstamo devuelto correctamente."
        });
    }
}
