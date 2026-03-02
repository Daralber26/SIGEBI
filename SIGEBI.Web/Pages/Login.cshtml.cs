using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIGEBI.Contracts.Auth;

namespace SIGEBI.Web.Pages;

public class LoginModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty, Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [BindProperty, Required]
    public string Password { get; set; } = string.Empty;

    public string? Error { get; set; }

    public IActionResult OnGet()
    {
        // Si ya hay sesión activa, redirigir al Index
        if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            return RedirectToPage("/Index");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Error = "Completa email y password.";
            return Page();
        }

        var client = _httpClientFactory.CreateClient("SIGEBI.Api");

        var request = new LoginRequest
        {
            Email = Email.Trim(),
            Password = Password
        };

        HttpResponseMessage resp;

        try
        {
            resp = await client.PostAsJsonAsync("auth/login", request);
        }
        catch
        {
            Error = "No se pudo conectar a la API. Asegúrate que SIGEBI.Api esté corriendo en http://localhost:5016.";
            return Page();
        }

        if (resp.StatusCode == HttpStatusCode.Unauthorized)
        {
            Error = "Credenciales inválidas.";
            return Page();
        }

        if (!resp.IsSuccessStatusCode)
        {
            Error = $"Error en login ({(int)resp.StatusCode}).";
            return Page();
        }

        var user = await resp.Content.ReadFromJsonAsync<LoginResponse>();

        if (user is null || user.Id == Guid.Empty)
        {
            Error = "Login respondió vacío o inválido.";
            return Page();
        }

        // Guardar sesión
        HttpContext.Session.SetString("UserId", user.Id.ToString());
        HttpContext.Session.SetString("UserNombre", user.Nombre ?? string.Empty);
        HttpContext.Session.SetString("UserEmail", user.Email ?? string.Empty);

        return RedirectToPage("/Index");
    }
}