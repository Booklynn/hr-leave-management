using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "3f5f8a64-2f1e-4c55-9d1c-861fef36be79",
                UserId = "a62e5c90-6fc5-49f3-ae14-41c6cb3424b9"
            },
            new IdentityUserRole<string>
            {
                RoleId = "3f9a6bba-5d7a-4a70-b5b1-cc6c15f1f87d",
                UserId = "7be551b9-05b3-41cf-addb-46630242f0ee"
            }
        );
    }
}
