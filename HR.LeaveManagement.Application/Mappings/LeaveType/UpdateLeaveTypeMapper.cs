using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

namespace HR.LeaveManagement.Application.Mappings.LeaveType;

public class UpdateLeaveTypeMapper : IUpdateMapper<UpdateLeaveTypeCommand, Domain.LeaveType>
{
    public void Map(UpdateLeaveTypeCommand dto, Domain.LeaveType leaveType)
    {
        leaveType.Name = dto.Name;
        leaveType.DefaultDays = dto.DefaultDays;
    }
}
