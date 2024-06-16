using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Common.Settings;
using CleanArchitecture.Identity.Models;
using CleanArchitecture.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.Identity;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.LoadConfiguration<JWtSettings>();
        services.Configure<JWtSettings>(configuration.GetSection(new JWtSettings().SettingName));

        services.AddDbContext<StreamerIdentityDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"),
            m => m.MigrationsAssembly(typeof(StreamerIdentityDbContext).Assembly.FullName))
        );

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<StreamerIdentityDbContext>().AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });

        return services;
    }
}