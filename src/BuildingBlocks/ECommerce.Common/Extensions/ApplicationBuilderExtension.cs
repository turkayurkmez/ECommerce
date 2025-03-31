using ECommerce.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ECommerce.Common.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
