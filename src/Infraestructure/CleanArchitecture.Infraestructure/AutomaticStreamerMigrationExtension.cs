using CleanArchitecture.Infraestructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infraestructure;

public static class AutomaticStreamerMigrationExtension
{
    public static IApplicationBuilder UseAutomaticStreamerMigration(this IApplicationBuilder app)
    {
        using var scopoe = app.ApplicationServices.CreateAsyncScope();
        var context = scopoe.ServiceProvider.GetRequiredService<StreamerDbContext>();
        context.Database.Migrate();
        return app;
    }
}