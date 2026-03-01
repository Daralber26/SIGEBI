using SIGEBI.Contracts.Auth;
using SIGEBI.Desktop.Modules.Auth.Interfaces;
using SIGEBI.Desktop.Shared;

namespace SIGEBI.Desktop.Modules.Auth.Services;

public sealed class AuthService : IAuthService
{
    private readonly ApiClient _api;
    private readonly SessionStore _session;

    public AuthService(ApiClient api, SessionStore session)
    {
        _api = api;
        _session = session;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var res = await _api.PostAsync<LoginRequest, LoginResponse>("auth/login", request, ct);

        _session.Start(res.Id, res.Nombre, res.Email);

        return res;
    }

    public void Logout() => _session.Clear();
}