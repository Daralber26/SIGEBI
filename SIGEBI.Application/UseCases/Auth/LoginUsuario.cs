using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Auth;
using System.Security.Cryptography;
using System.Text;

namespace SIGEBI.Application.UseCases.Auth;

public class LoginUsuario
{
    private readonly IUsuarioRepository _usuarios;

    public LoginUsuario(IUsuarioRepository usuarios)
    {
        _usuarios = usuarios;
    }

    public async Task<LoginResponse?> Ejecutar(LoginRequest request, CancellationToken ct)
    {
        var usuario = await _usuarios.GetByEmailAsync(request.Email, ct);
        if (usuario is null) return null;

        var passwordHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(request.Password))
        );

        if (usuario.PasswordHash != passwordHash) return null;

        return new LoginResponse
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email
        };
    }
}
