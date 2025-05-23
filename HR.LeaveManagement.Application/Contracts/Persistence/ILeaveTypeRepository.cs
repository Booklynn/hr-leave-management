using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeUnique(string name);
    Task<bool> IsLeaveTypeUnique(int id, string name);
    Task<bool> DoesLeaveTypeExist(int id);
}
