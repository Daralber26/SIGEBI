using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.UseCases.Ejemplares;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EjemplaresController : ControllerBase
{
    private readonly CrearEjemplar _crearEjemplar;

    public EjemplaresController(CrearEjemplar crearEjemplar)
    {
        _crearEjemplar = crearEjemplar;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearEjemplarDto dto, CancellationToken ct)
    {
        var id = await _crearEjemplar.EjecutarAsync(dto.RecursoId, dto.Codigo, ct);
        return Ok(new { Id = id });
    }

    public class CrearEjemplarDto
    {
        public Guid RecursoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }

}
