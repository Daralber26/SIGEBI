using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace SIGEBI.Api.Middleware;

public sealed class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ApiExceptionMiddleware(
        RequestDelegate next,
        ILogger<ApiExceptionMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception. TraceId={TraceId}", context.TraceIdentifier);

        var (statusCode, title) = MapException(ex);

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = GetSafeDetail(ex),
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }

    private static (int StatusCode, string Title) MapException(Exception ex)
    {
        return ex switch
        {
            ArgumentException => ((int)HttpStatusCode.BadRequest, "Solicitud inválida"),
            KeyNotFoundException => ((int)HttpStatusCode.NotFound, "Recurso no encontrado"),
            InvalidOperationException => ((int)HttpStatusCode.Conflict, "Conflicto de operación"),
            _ => ((int)HttpStatusCode.InternalServerError, "Error interno del servidor")
        };
    }

    private string GetSafeDetail(Exception ex)
    {// En prod NO se filtra stacktrace jamás.
        if (!_env.IsDevelopment())
            return ex switch
            {
                ArgumentException => ex.Message,
                InvalidOperationException => ex.Message,
                KeyNotFoundException => ex.Message,
                _ => "Ocurrió un error inesperado. Usa el traceId para soporte."
            };

        // En development sí queremos todo para depurar
        return ex.ToString();
    }
}