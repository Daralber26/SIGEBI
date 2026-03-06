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
    public async Task<IActionResult> Registrar([FromBody] RegisterUserRequest request, CancellationToken ct)
    {
        // validación mínima
        if (request is null)
            return BadRequest("Request inválido.");

        if (string.IsNullOrWhiteSpace(request.Nombre) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Nombre, email y password son obligatorios.");
        }

        // hash simple (para clase)
        var passwordHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(request.Password))
        );

        // NO object initializer: tu Usuario es de solo lectura (private set)
        var usuario = new Usuario(
            Guid.NewGuid(),
            request.Nombre,
            request.Email,
            passwordHash
        );

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync(ct);

        // devolvemos sin password
        return Ok(new
        {
            usuario.Id,
            usuario.Nombre,
            usuario.Email,
            usuario.Activo,
            FechaRegistro = usuario.FechaRegistro // si tu propiedad se llama FechaRegistro
            // Si en tu entidad se llama FechaRegistroUtc, cambia a:
            // FechaRegistro = usuario.FechaRegistroUtc
        });
    }
}