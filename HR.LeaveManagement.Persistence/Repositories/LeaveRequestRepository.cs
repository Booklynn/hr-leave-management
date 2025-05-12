using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository(HrDatabaseContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    public async Task<IReadOnlyList<LeaveRequest>> GetListLeaveRequestsWithDetails()
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Include(query => query.LeaveType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetListLeaveRequestsWithDetails(string userId)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Where(query => query.RequestingEmployeeId == userId)
            .Include(query => query.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Include(query => query.LeaveType)
            .FirstOrDefaultAsync(query => query.Id == id);
    }
}
