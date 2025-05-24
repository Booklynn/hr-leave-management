using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class InMemoryHrDatabaseContextTests
{
    private readonly BaseHrDatabaseContext _hrDatabaseContext;

    public InMemoryHrDatabaseContextTests()
    {
        _hrDatabaseContext = InMemoryHrDatabaseContextFactory.Create();
    }

    [Fact]
    public async Task SaveChangesAsync_SetDateCreated()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            Name = "Test Vacation Leave",
            DefaultDays = 7
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task SaveChangesAsync_SetDateModified()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            Name = "Test Vacation Leave",
            DefaultDays = 7
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateModified.ShouldNotBeNull();
    }
}
