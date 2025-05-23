using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class BaseHrDatabaseContext(DbContextOptions<BaseHrDatabaseContext> options) : DbContext(options)
{
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseHrDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(entityEntry => entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified))
        {
            var currentDateTime = DateTime.Now;

            entry.Entity.DateModified = currentDateTime;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = currentDateTime;
            }
        }
               
        return base.SaveChangesAsync(cancellationToken);
    }
}
