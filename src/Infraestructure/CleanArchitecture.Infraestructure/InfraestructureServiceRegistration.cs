using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Infraestructure.Persistence;
using CleanArchitecture.Infraestructure.Repositories;
using CleanArchitecture.Infraestructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infraestructure;

public static class InfraestructureServiceRegistration
{
    public static IServiceCollection AddInfraestructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StreamerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
            m => m.MigrationsAssembly(typeof(StreamerDbContext).Assembly.FullName))
        );
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IStreamerRepository, StreamerRepository>();

        services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}