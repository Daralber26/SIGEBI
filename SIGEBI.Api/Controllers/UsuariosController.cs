using Microsoft.AspNetCore.Mvc;
using SIGEBI.Contracts.Users;
using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Persistence;
using System.Security.Cryptography;
using System.Text;

namespace SIGEBI.Api.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsuariosController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(RegisterUserRequest request, CancellationToken ct)
    {
        // hash simple (para clase)
        var passwordHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(request.Password))
        );

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = passwordHash,
            Activo = true,
            FechaRegistro = DateTime.UtcNow
        };

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync(ct);

        // devolvemos sin password
        return Ok(new
        {
            usuario.Id,
            usuario.Nombre,
            usuario.Email,
            usuario.Activo,
            usuario.FechaRegistro
        });
    }
}
