using System.Net.Http.Json;

namespace SIGEBI.Desktop.Shared;

public sealed class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest body, CancellationToken ct = default)
    {
        using var response = await _http.PostAsJsonAsync(path, body, ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(ct);
            throw new ApiException((int)response.StatusCode, error);
        }

        var data = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: ct);
        return data ?? throw new ApiException((int)response.StatusCode, "Respuesta vacía del servidor.");
    }
}

public sealed class ApiException : Exception
{
    public int StatusCode { get; }
    public ApiException(int statusCode, string message) : base(message) => StatusCode = statusCode;
}