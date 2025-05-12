using HR.LeaveManagement.Application.Common;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommand : IRequest<int>
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period  { get; set; }
}
