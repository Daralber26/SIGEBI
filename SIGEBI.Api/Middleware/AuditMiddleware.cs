using SIGEBI.Infrastructure.Persistence;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Api.Middleware;

public sealed class AuditMiddleware
{
    private readonly RequestDelegate _next;

    public AuditMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Ejecuta el resto del pipeline primero
        await _next(context);

        // Evita registrar swagger/archivos estáticos si quieres
        var path = context.Request.Path.Value ?? "";

        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
            return;

        // Solo registra lo importante (puedes cambiar esto)
        var method = context.Request.Method.ToUpperInvariant();
        var shouldAudit =
            method is "POST" or "PUT" or "DELETE" ||
            path.StartsWith("/api", StringComparison.OrdinalIgnoreCase);

        if (!shouldAudit)
            return;

        // Sacar datos
        var traceId = context.TraceIdentifier;
        var status = context.Response.StatusCode;
        var ip = context.Connection.RemoteIpAddress?.ToString();

        // Como no tienes auth real, usuario será null (luego lo mejoramos)
        string? usuario = null;

        // “Detalle” opcional: por ahora usa el endpoint
        string? detalle = $"{method} {path}";

        // Guardar en BD
        // IMPORTANTÍSIMO: usar un scope porque AppDbContext es scoped
        using var scope = context.RequestServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        db.Auditorias.Add(new Auditoria(
            metodo: method,
            ruta: path,
            statusCode: status,
            traceId: traceId,
            ip: ip,
            usuario: usuario,
            detalle: detalle
        ));

        await db.SaveChangesAsync();
    }
}