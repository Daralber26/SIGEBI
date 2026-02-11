using Microsoft.AspNetCore.Mvc;

namespace SIGEBI.Api.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "OK",
                api = "SIGEBI.Api",
                time = DateTime.UtcNow
            });
        }
    }
}