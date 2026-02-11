using Microsoft.AspNetCore.Mvc;
using SIGEBI.Contracts;
using SIGEBI.Contracts.Dependencias;

namespace SIGEBI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DependenciasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var dependencia = new DependenciaDto
            {
                Id = 1,
                Nombre = "Finanzas"
            };

            return Ok(dependencia);
        }
    }
}