using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository(BaseHrDatabaseContext context) : GenericRepository<LeaveType>(context), ILeaveTypeRepository
{
    public async Task<bool> DoesLeaveTypeExist(int id)
    {
        return await _context.LeaveTypes
            .AsNoTracking()
            .AnyAsync(query => query.Id == id);
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return !await _context.LeaveTypes
            .AsNoTracking()
            .AnyAsync(query => query.Name == name);
    }

    public async Task<bool> IsLeaveTypeUnique(int id, string name)
    {
        return !await _context.LeaveTypes
            .AsNoTracking()
            .AnyAsync(lt => lt.Name == name && lt.Id != id);
    }
}
