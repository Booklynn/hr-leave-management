using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id);
    Task<IReadOnlyList<LeaveAllocation>> GetListLeaveAllocationsWithDetails();
    Task<IReadOnlyList<LeaveAllocation>> GetListLeaveAllocationsWithDetails(string userId);
    Task<bool> DoesAllocationExisits(string userId, int leaveTypeId, int period);
    Task AddAllocations(IReadOnlyList<LeaveAllocation> allocations);
    Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId);
    Task<bool> DoesLeaveTypeExist(int id);
}
