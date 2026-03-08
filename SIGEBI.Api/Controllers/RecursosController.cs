using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Interfaces;
using SIGEBI.Contracts.Resources;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("recursos")]
public class RecursosController : ControllerBase
{
    private readonly IRecursoService _recursoService;

    public RecursosController(IRecursoService recursoService)
    {
        _recursoService = recursoService;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        CreateResourceRequest request,
        CancellationToken ct)
    {
        await _recursoService.AddAsync(new SIGEBI.Application.Dtos.Recursos.SaveRecursoDto
        {
            Titulo = request.Titulo,
            Autor = request.Autor,
            Isbn = request.Isbn
        }, ct);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(
        Guid id,
        UpdateResourceRequest request,
        CancellationToken ct)
    {
        await _recursoService.UpdateAsync(new SIGEBI.Application.Dtos.Recursos.UpdateRecursoDto
        {
            Id = id,
            Titulo = request.Titulo,
            Autor = request.Autor,
            Isbn = request.Isbn
        }, ct);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(
        Guid id,
        CancellationToken ct)
    {
        await _recursoService.SoftDeleteAsync(new SIGEBI.Application.Dtos.Recursos.RemoveRecursoDto
        {
            Id = id
        }, ct);

        return NoContent();
    }
}