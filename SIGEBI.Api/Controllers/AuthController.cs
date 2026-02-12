using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.UseCases.Auth;
using SIGEBI.Contracts.Auth;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly LoginUsuario _login;

    public AuthController(LoginUsuario login)
    {
        _login = login;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
    {
        var result = await _login.Ejecutar(request, ct);

        if (result is null)
            return Unauthorized("Credenciales inválidas");

        return Ok(result);
    }
}
