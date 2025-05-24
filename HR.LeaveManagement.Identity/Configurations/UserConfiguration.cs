using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser
            {
                Id = "3f9a6bba-5d7a-4a70-b5b1-cc6c15f1f87d",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "P@ssw0rd123"),
                SecurityStamp = "d4e5f8a0-9c3b-4b17-8c45-1f2a3b4c5d6e",
                ConcurrencyStamp = "123e4567-e89b-12d3-a456-426614174000"
            },
            new ApplicationUser
            {
                Id = "a62e5c90-6fc5-49f3-ae14-41c6cb3424b9",
                Email = "user1@localhost.com",
                NormalizedEmail = "USER1@LOCALHOST.COM",
                FirstName = "User",
                LastName = "One",
                UserName = "user1@localhost.com",
                NormalizedUserName = "USER1@LOCALHOST.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "P@ssw0rd123"),
                SecurityStamp = "a1b2c3d4-e5f6-7890-abcd-1234567890ef",
                ConcurrencyStamp = "fedcba98-7654-3210-fedc-ba9876543210"
            }
        );
    }
}
