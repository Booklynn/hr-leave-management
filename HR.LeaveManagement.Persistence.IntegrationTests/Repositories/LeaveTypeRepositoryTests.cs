using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests.Repositories;

public class LeaveTypeRepositoryTests
{
    private readonly SqliteInMemoryHrDatabaseContextFactory _testFactory;
    private readonly BaseHrDatabaseContext _hrDatabaseContext;
    private readonly LeaveTypeRepository _leaveTypeRepository;

    public LeaveTypeRepositoryTests()
    {
        _testFactory = new SqliteInMemoryHrDatabaseContextFactory();
        _hrDatabaseContext = _testFactory.Context;
        _leaveTypeRepository = new LeaveTypeRepository(_hrDatabaseContext);
    }

    [Fact]
    public async Task IsLeaveTypeUnique_FindNewName_ReturnsTrue()
    {
        var result = await _leaveTypeRepository.IsLeaveTypeUnique("Test New Leave");
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task IsLeaveTypeUnique_FindDuplicateName_ReturnsFalse()
    {
        var result = await _leaveTypeRepository.IsLeaveTypeUnique("Vacation");
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task IsLeaveTypeUnique_FindCurrentEntity_ReturnsTrue()
    {
        var result = await _leaveTypeRepository.IsLeaveTypeUnique(1, "Vacation");
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task IsLeaveTypeUnique_FindCurrentIdWithNewName_ReturnsTrue()
    {
        var result = await _leaveTypeRepository.IsLeaveTypeUnique(1, "Sick");
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task IsLeaveTypeUnique_FindCurrentIdWithDuplicateName_ReturnsFalse()
    {
        await GenerateTestData();
        var result = await _leaveTypeRepository.IsLeaveTypeUnique(1, "Business");
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task DoesLeaveTypeExist_FindExistingId_ReturnsTrue()
    {
        var result = await _leaveTypeRepository.DoesLeaveTypeExist(1);
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task DoesLeaveTypeExist_FindNotExistingId_ReturnsFalse()
    {
        var result = await _leaveTypeRepository.DoesLeaveTypeExist(2);
        result.ShouldBeFalse();
    }

    private async Task GenerateTestData()
    {
        var leaveTypes = new LeaveType[]
        {
            new() {
                Name = "Business",
                DefaultDays = 7,
            }
        };

        foreach (var item in leaveTypes)
        {
            await _hrDatabaseContext.LeaveTypes.AddAsync(item);
            await _hrDatabaseContext.SaveChangesAsync();
        }
    }
}