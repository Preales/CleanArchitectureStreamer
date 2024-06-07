using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "d81b8eea-04f1-436a-8662-73e8de1632bd",
                UserId = "d10a2f15-a4ef-4f94-9e05-d116db6e5019"
            },new IdentityUserRole<string>
            {
                RoleId = "775b4851-a07f-4326-b31d-ec20ec3be6c4",
                UserId = "c8b996d7-e738-4b4c-aa17-8520360d394a"
            }
        );
    }
}