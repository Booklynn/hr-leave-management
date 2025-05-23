using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class HrDatabaseContextSqlServer(DbContextOptions<BaseHrDatabaseContext> options) : BaseHrDatabaseContext(options)
{
}
