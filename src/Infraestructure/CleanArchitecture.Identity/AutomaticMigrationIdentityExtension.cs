using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Identity;

public static class AutomaticMigrationIdentityExtension
{
    public static IApplicationBuilder UseAutomaticIdentityMigration(this IApplicationBuilder app)
    {
        using var scopoe = app.ApplicationServices.CreateAsyncScope();
        var context = scopoe.ServiceProvider.GetRequiredService<StreamerIdentityDbContext>();
        context.Database.Migrate();
        return app;
    }
}
