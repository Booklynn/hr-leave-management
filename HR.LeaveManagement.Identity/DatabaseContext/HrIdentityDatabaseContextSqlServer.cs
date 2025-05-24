using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.DatabaseContext;

public class HrIdentityDatabaseContextSqlServer(DbContextOptions<BaseHrIdentityDatabaseContext> options) : BaseHrIdentityDatabaseContext(options)
{
}
