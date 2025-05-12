using HR.LeaveManagement.Application.Features.LeaveType.DTO;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.DTO;

public class LeaveAllocationDetailsDTO
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public LeaveTypeDTO? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}
