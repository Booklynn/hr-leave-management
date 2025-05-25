using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "3f5f8a64-2f1e-4c55-9d1c-861fef36be79",
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
            },
            new IdentityRole
            {
                Id = "3f9a6bba-5d7a-4a70-b5b1-cc6c15f1f87d",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            }
        );
    }
}
