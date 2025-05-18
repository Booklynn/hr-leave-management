using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public static class HrDbContextTestFactory
{
    public static HrDatabaseContext Create()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new HrDatabaseContext(dbOptions);
    }
}
