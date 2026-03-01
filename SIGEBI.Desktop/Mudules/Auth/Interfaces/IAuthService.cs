using SIGEBI.Contracts.Auth;

namespace SIGEBI.Desktop.Modules.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
    void Logout();
}