using HR.LeaveManagement.BlazorWASM.Models.LeaveTypes;
using HR.LeaveManagement.BlazorWASM.Services.Base;

namespace HR.LeaveManagement.BlazorWASM.Contracts;

public interface ILeaveTypeService
{
    Task<IReadOnlyList<LeaveTypeViewModel>> GetLeaveTypes();
    Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id);
    Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType);
    Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType);
    Task<Response<Guid>> DeleteLeaveType(int id);
}
