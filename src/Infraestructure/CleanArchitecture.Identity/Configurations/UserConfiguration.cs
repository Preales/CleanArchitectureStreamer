using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "d10a2f15-a4ef-4f94-9e05-d116db6e5019",
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com",
                Nombre = "Admin",
                Apellidos = "Admin",
                UserName = "admin",
                NormalizedUserName = "admin",
                PasswordHash = hasher.HashPassword(null, "Adm1n*.")
            },
            new ApplicationUser
            {
                Id = "c8b996d7-e738-4b4c-aa17-8520360d394a",
                Email = "soporte@localhost.com",
                NormalizedEmail = "soporte@localhost.com",
                Nombre = "Soporte",
                Apellidos = "Soporte",
                UserName = "soporte",
                NormalizedUserName = "soporte",
                PasswordHash = hasher.HashPassword(null, "Adm1n*.")
            }
        );
    }
}