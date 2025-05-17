using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration.GetValue<string>("DbProvider");

        services.AddDbContext<HrDatabaseContext>(options =>
        {
            switch (provider?.ToLowerInvariant())
            {
                case "sqlserver":
                    options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
                    break;

/*                case "sqlite":
                    options.UseSqlite(config.GetConnectionString("HrDatabaseSqliteConnectionString"));
                    break;*/

                case "inmemory":
                    options.UseInMemoryDatabase("TestDb");
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported provider: {provider}");
            }
            ;
        });
       
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

        return services;
    }
}
