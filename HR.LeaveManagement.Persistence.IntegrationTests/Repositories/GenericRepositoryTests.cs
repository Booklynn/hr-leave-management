using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests.Repositories;

public class GenericRepositoryTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;
    private readonly GenericRepository<LeaveType> _genericRepository;

    public GenericRepositoryTests()
    {
        _hrDatabaseContext = HrDbContextTestFactory.Create();
        _genericRepository = new GenericRepository<LeaveType>(_hrDatabaseContext);
    }

    [Fact]
    public async Task GetListAsync_ReturnsList()
    {
        await GenerateTestData();

        var result = await _genericRepository.GetListAsync();

        result.ShouldBeAssignableTo<IReadOnlyList<LeaveType>>();
        result.Count.ShouldBeGreaterThan(0);
        result[0].Id.ShouldBe(1);
        result[1].Id.ShouldBe(2);
    }

    [Fact]
    public async Task GetListAsync_ReturnsEmptyList()
    {
        var result = await _genericRepository.GetListAsync();

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEntity()
    {
        await GenerateTestData();

        var result = await _genericRepository.GetByIdAsync(2);

        result.ShouldNotBeNull();
        result.Name.ShouldBe("Test Sick Leave");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull()
    {
        var result = await _genericRepository.GetByIdAsync(2);

        result.ShouldBeNull();
    }

    [Fact]
    public async Task CreateAsync_SaveEntity()
    {
        var leaveType = new LeaveType
        {
            Id = 2,
            Name = "Test Sick Leave",
            DefaultDays = 3
        };

        await _genericRepository.CreateAsync(leaveType);
        
        var result = await _hrDatabaseContext.LeaveTypes.FindAsync(leaveType.Id);
        result.ShouldNotBeNull();
        result.Name.ShouldBeEquivalentTo(leaveType.Name);
    }

    [Fact]
    public async Task UpdateAsync_UpdateEntity()
    {
        await GenerateTestData();
        var leaveType = await _hrDatabaseContext.LeaveTypes.FindAsync(3);
        leaveType.ShouldNotBeNull();
        leaveType.DefaultDays = 4;

        await _genericRepository.UpdateAsync(leaveType);
        
        var result = await _hrDatabaseContext.LeaveTypes.FindAsync(leaveType.Id);
        result.ShouldNotBeNull();
        result.DefaultDays.ShouldBe(leaveType.DefaultDays);
        result.DateCreated.ShouldBe(leaveType.DateCreated);
    }

    [Fact]
    public async Task DeleteAsync_DeleteEntity()
    {
        await GenerateTestData();
        var leaveType = await _hrDatabaseContext.LeaveTypes.FindAsync(1);
        leaveType.ShouldNotBeNull();

        await _genericRepository.DeleteAsync(leaveType);

        var result = await _hrDatabaseContext.LeaveTypes.FindAsync(leaveType.Id);
        result.ShouldBeNull();
    }

    private async Task GenerateTestData()
    {
        var leaveTypes = new LeaveType[]
        {
            new() {
                Id = 1,
                Name = "Test Vacation Leave",
                DefaultDays = 7,
            },
            new() {
                Id = 2,
                Name = "Test Sick Leave",
                DefaultDays = 3
            },
            new() {
                Id = 3,
                Name = "Test ETC",
                DefaultDays = 3
            }
        };

        foreach (var item in leaveTypes)
        {
            await _hrDatabaseContext.LeaveTypes.AddAsync(item);
            await _hrDatabaseContext.SaveChangesAsync();
        }
    }
}
