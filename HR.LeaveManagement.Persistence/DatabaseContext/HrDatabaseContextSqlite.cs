using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class HrDatabaseContextSqlite(DbContextOptions<BaseHrDatabaseContext> options) : BaseHrDatabaseContext(options)
{
}
