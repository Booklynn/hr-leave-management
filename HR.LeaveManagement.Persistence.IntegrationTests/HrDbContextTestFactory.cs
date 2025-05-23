using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public static class HrDbContextTestFactory
{
    public static BaseHrDatabaseContext Create()
    {
        var dbOptions = new DbContextOptionsBuilder<BaseHrDatabaseContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new BaseHrDatabaseContext(dbOptions);
    }
}
