using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveType;

public class UpdateLeaveTypeMapper : IUpdateMapper<UpdateLeaveTypeCommand, Domain.LeaveType>
{
    public Domain.LeaveType Map(UpdateLeaveTypeCommand dto, Domain.LeaveType existingLeaveType)
    {
        existingLeaveType.Name = dto.Name;
        existingLeaveType.DefaultDays = dto.DefaultDays;
        return existingLeaveType;
    }
}
