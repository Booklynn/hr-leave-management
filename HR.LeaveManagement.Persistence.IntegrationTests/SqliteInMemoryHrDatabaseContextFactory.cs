using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class SqliteInMemoryHrDatabaseContextFactory
{
    private readonly SqliteConnection _connection;
    public BaseHrDatabaseContext Context { get; }

    public SqliteInMemoryHrDatabaseContextFactory()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<BaseHrDatabaseContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new BaseHrDatabaseContext(options);
        Context.Database.EnsureCreated();
    }

    public void CloseConnection () => _connection.Close();
}
