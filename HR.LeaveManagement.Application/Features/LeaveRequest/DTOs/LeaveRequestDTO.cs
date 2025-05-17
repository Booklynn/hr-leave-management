using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;

public class LeaveRequestDTO : BaseLeaveRequestDTO
{
    public string RequestingEmployeeId {  get; set; } = string.Empty;
    public LeaveTypeDTO? LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public bool? Approved { get; set; }
}
