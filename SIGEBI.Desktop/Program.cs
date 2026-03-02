using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SIGEBI.Desktop.Forms;
using SIGEBI.Desktop.Modules.Auth.Interfaces;
using SIGEBI.Desktop.Modules.Auth.Services;
using SIGEBI.Desktop.Modules.Catalogo.Interfaces;
using SIGEBI.Desktop.Modules.Catalogo.Services;
using SIGEBI.Desktop.Shared;

namespace SIGEBI.Desktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(cfg =>
                {
                    cfg.SetBasePath(AppContext.BaseDirectory);
                    cfg.AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureServices((ctx, services) =>
                {
                    services.Configure<ApiOptions>(ctx.Configuration.GetSection("Api"));

                    services.AddSingleton<SessionStore>();

                    services.AddHttpClient<ApiClient>((sp, http) =>
                    {
                        var opt = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                        http.BaseAddress = new Uri(opt.BaseUrl);
                    });

                    services.AddScoped<IAuthService, AuthService>();

                    //  Catalogo
                    services.AddScoped<ICatalogoService, CatalogoService>();

                    //Services

                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<CatalogoForm>();

                    // Si MainForm crea/abre CatalogoForm desde DI,
                    // también tendrás que registrar CatalogoForm (lo vemos en el paso 4).
                })
                .Build();

            Application.Run(host.Services.GetRequiredService<LoginForm>());
        }
    }
}