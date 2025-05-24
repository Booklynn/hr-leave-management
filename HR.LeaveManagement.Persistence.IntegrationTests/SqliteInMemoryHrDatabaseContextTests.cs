using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class SqliteInMemoryHrDatabaseContextTests
{
    private readonly SqliteInMemoryHrDatabaseContextFactory _testFactory;

    public SqliteInMemoryHrDatabaseContextTests()
    {
        _testFactory = new SqliteInMemoryHrDatabaseContextFactory();
    }

    [Fact]
    public async Task InitDatabase_EnsureHaveData()
    {
        var leaveType = await _testFactory.Context.LeaveTypes.ToArrayAsync();

        leaveType.ShouldNotBeNull();
        leaveType.ShouldNotBeEmpty();

        _testFactory.CloseConnection();
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

        _testFactory.CloseConnection();
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

        _testFactory.CloseConnection();
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

        _testFactory.CloseConnection();
    }
}
