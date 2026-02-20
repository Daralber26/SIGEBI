using SIGEBI.Api.Middleware;

namespace SIGEBI.Api;

public static class ApiExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ApiExceptionMiddleware>();
}