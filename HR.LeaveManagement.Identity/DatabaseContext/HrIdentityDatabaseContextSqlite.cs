using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.DatabaseContext;

public class HrIdentityDatabaseContextSqlite(DbContextOptions<BaseHrIdentityDatabaseContext> options) : BaseHrIdentityDatabaseContext(options)
{
}
