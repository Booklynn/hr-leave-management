using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.DatabaseContext;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HR.LeaveManagement.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetRequiredSection("JWTSettings"));

        var provider = configuration.GetValue<string>("DbProvider");
        switch (provider?.ToLowerInvariant())
        {
            case "sqlserver":
                services.AddDbContext<BaseHrIdentityDatabaseContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString")));
                break;

            case "sqlite":
                services.AddDbContext<BaseHrIdentityDatabaseContext>(options =>
                {
                    options.UseSqlite(configuration.GetConnectionString("HrDatabaseConnectionString"));
                    options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
                });
                break;

            default:
                throw new InvalidOperationException($"Unsupported provider: {provider}");
        }

        services.AddIdentity<ApplicationUser, IdentityRole>().
            AddEntityFrameworkStores<BaseHrIdentityDatabaseContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidAudience = configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"] ?? string.Empty))
            };
        });

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
