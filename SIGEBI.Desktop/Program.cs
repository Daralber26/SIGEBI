using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SIGEBI.Desktop.Forms;
using SIGEBI.Desktop.Modules.Auth.Interfaces;
using SIGEBI.Desktop.Modules.Auth.Services;
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

                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                })
                .Build();

            Application.Run(host.Services.GetRequiredService<LoginForm>());
        }
    }
}