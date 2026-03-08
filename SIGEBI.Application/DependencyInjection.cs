using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Application.UseCases.Recursos;

namespace SIGEBI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRecursoService, RecursoService>();

        services.AddScoped<CrearRecurso>();
        services.AddScoped<ActualizarRecurso>();
        services.AddScoped<EliminarRecurso>();

        return services;
    }
}