using System;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

//Session (para guardar usuario logueado)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//HttpClient hacia la API
builder.Services.AddHttpClient("SIGEBI.Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7010/");
});

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Session debe ir DESPUÉS de Routing y ANTES de Authorization/MapRazorPages
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();