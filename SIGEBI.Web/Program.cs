var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// Session (para guardar usuario logueado)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpClient hacia la API (DEV: usa HTTP para evitar líos de SSL)
builder.Services.AddHttpClient("SIGEBI.Api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5016/");
});

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// En DEV puedes dejar esto, pero no es obligatorio si usas http hacia API.
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

// Session debe ir después de Routing
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();