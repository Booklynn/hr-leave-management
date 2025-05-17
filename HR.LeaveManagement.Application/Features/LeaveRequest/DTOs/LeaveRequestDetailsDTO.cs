using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;

public class LeaveRequestDetailsDTO : BaseLeaveRequestDTO
{
    public LeaveTypeDTO? LeaveType { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
    public DateTime DateRequested { get; set; }
    public DateTime? DateActioned { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
}
