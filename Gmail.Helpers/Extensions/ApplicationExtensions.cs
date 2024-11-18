using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gmail.Helpers.Extensions
{
    public static class ApplicationExtensions
    {
        public static void MigrateDatabase<T>(this WebApplication app) where T : DbContext
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<T>();
                context.Database.SetCommandTimeout(600);
                context.Database.Migrate();
            }
        }

        public static IApplicationBuilder CustomUseForwardedHeaders(this WebApplication app)
        {
            return app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
