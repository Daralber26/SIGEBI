using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.UseCases.Reservas;
using SIGEBI.Contracts.Reservas;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ReservasController : ControllerBase
{
    private readonly CrearReserva _crearReserva;

    public ReservasController(CrearReserva crearReserva)
    {
        _crearReserva = crearReserva;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateReservaRequest request, CancellationToken ct)
    {
        var id = await _crearReserva.HandleAsync(request, ct);
        return CreatedAtAction(nameof(Crear), new { id }, new { id });
    }
}