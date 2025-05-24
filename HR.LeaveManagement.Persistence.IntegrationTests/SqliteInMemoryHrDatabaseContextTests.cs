using HR.LeaveManagement.Domain;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class SqliteInMemoryHrDatabaseContextTests : IDisposable
{
    private readonly SqliteInMemoryHrDatabaseContextFactory _testFactory;

    public SqliteInMemoryHrDatabaseContextTests()
    {
        _testFactory = new SqliteInMemoryHrDatabaseContextFactory();
    }

    public void Dispose()
    {
        _testFactory.Dispose();
    }

    [Fact]
    public async Task SaveChangesAsync_SetDateCreated()
    {
        var leaveType = new LeaveType
        {
            Name = "Test Vacation Leave wuth Sqlite In-memory",
            DefaultDays = 7
        };

        await _testFactory.Context.LeaveTypes.AddAsync(leaveType);
        await _testFactory.Context.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task SaveChangesAsync_SetDateModified()
    {
        var leaveType = new LeaveType
        {
            Name = "Test Vacation Leave wuth Sqlite In-memory",
            DefaultDays = 7
        };

        await _testFactory.Context.LeaveTypes.AddAsync(leaveType);
        await _testFactory.Context.SaveChangesAsync();

        leaveType.DateModified.ShouldNotBeNull();
    }

    [Fact]
    public async Task SaveChangesAsync_AutoIncrementPrimaryKey()
    {
        var leaveType = new LeaveType
        {
            Name = "Test Vacation Leave wuth Sqlite In-memory",
            DefaultDays = 7
        };

        await _testFactory.Context.LeaveTypes.AddAsync(leaveType);
        await _testFactory.Context.SaveChangesAsync();

        leaveType.Id.ShouldBe(2);
    }
}
