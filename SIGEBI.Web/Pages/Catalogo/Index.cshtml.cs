using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIGEBI.Web.Pages.Catalogo;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public string? Error { get; private set; }

    public List<Dictionary<string, string>> Rows { get; private set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // Protección: solo usuarios logueados
        if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            return RedirectToPage("/Login");

        var client = _httpClientFactory.CreateClient("SIGEBI.Api");

        try
        {
            using var resp = await client.GetAsync("catalogo");

            if (!resp.IsSuccessStatusCode)
            {
                Error = $"Error cargando catálogo ({(int)resp.StatusCode}).";
                return Page();
            }

            var json = await resp.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                Error = "La API devolvió el catálogo vacío.";
                return Page();
            }

            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.ValueKind != JsonValueKind.Array)
            {
                Error = "Formato inesperado del catálogo (no es un arreglo).";
                return Page();
            }

            foreach (var item in doc.RootElement.EnumerateArray())
            {
                if (item.ValueKind != JsonValueKind.Object) continue;

                var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (var prop in item.EnumerateObject())
                    row[prop.Name] = prop.Value.ToString();

                Rows.Add(row);
            }

            return Page();
        }
        catch (Exception ex)
        {
            Error = $"No se pudo conectar a la API: {ex.Message}";
            return Page();
        }
    }

    public static string Get(Dictionary<string, string> row, params string[] keys)
    {
        foreach (var k in keys)
            if (row.TryGetValue(k, out var v) && !string.IsNullOrWhiteSpace(v))
                return v;

        return "";
    }
}