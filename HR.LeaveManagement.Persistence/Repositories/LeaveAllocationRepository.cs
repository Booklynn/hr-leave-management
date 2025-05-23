using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository(BaseHrDatabaseContext context) : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
{
    public async Task AddAllocations(IReadOnlyList<LeaveAllocation> allocations)
    {
        await _context.LeaveAllocations.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesAllocationExisits(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .AnyAsync(query => query.EmployeeId == userId 
                && query.LeaveTypeId == leaveTypeId 
                && query.Period == period);
    }

    public async Task<bool> DoesLeaveTypeExist(int id)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .AnyAsync(query => query.Id == id);
    }

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .Include(query => query.LeaveType)
            .FirstOrDefaultAsync(query => query.Id == id);
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetListLeaveAllocationsWithDetails()
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .Include(query => query.LeaveType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetListLeaveAllocationsWithDetails(string userId)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .Where(query => query.EmployeeId == userId)
            .Include(query => query.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .AsNoTracking()
            .FirstOrDefaultAsync(query => query.EmployeeId == userId 
                && query.LeaveTypeId == leaveTypeId);
    }
}
