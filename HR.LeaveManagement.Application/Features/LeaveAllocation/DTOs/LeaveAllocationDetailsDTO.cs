using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;

public class LeaveAllocationDetailsDTO
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public LeaveTypeDTO? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}
