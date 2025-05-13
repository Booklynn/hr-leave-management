using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

namespace HR.LeaveManagement.Application.Mappings.LeaveAllocation;

public class UpdateLeaveAllocationMapper : IUpdateMapper<UpdateLeaveAllocationCommand, Domain.LeaveAllocation>
{
    public void Map(UpdateLeaveAllocationCommand dto, Domain.LeaveAllocation leaveAllocation)
    {
        leaveAllocation.NumberOfDays = dto.NumberOfDays;
        leaveAllocation.LeaveTypeId = dto.LeaveTypeId;
        leaveAllocation.Period = dto.Period;
    }
}
