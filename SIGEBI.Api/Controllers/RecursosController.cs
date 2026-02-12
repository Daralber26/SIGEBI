using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.UseCases.Recursos;
using SIGEBI.Contracts.Resources;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("recursos")]
public class RecursosController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Crear(
        CreateResourceRequest request,
        [FromServices] CrearRecurso crear,
        CancellationToken ct)
    {
        var recurso = await crear.Ejecutar(request, ct);
        return Ok(recurso);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(
        Guid id,
        UpdateResourceRequest request,
        [FromServices] ActualizarRecurso actualizar,
        CancellationToken ct)
    {
        var ok = await actualizar.Ejecutar(id, request, ct);
        if (!ok) return NotFound("Recurso no encontrado");

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(
        Guid id,
        [FromServices] EliminarRecurso eliminar,
        CancellationToken ct)
    {
        var ok = await eliminar.Ejecutar(id, ct);
        if (!ok) return NotFound("Recurso no encontrado");

        return NoContent();
    }
}
