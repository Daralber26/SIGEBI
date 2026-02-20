using SIGEBI.Api.Middleware;

namespace SIGEBI.Api;

public static class AuditMiddlewareExtensions
{
    public static IApplicationBuilder UseApiAuditing(this IApplicationBuilder app)
        => app.UseMiddleware<AuditMiddleware>();
}