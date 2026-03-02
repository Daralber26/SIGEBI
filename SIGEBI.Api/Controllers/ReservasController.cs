using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.UseCases.Reservas;
using SIGEBI.Contracts.Reservas;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ReservaResponse>> Crear(
        [FromBody] CreateReservaRequest request,
        [FromServices] CrearReserva useCase,
        CancellationToken ct)
    {
        try
        {
            var id = await useCase.ExecuteAsync(request.UsuarioId, request.RecursoId, ct);
            return Ok(new ReservaResponse { Id = id });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}