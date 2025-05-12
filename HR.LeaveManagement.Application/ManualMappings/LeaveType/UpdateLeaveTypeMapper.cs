using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveType;

public class UpdateLeaveTypeMapper : IManualMapper<UpdateLeaveTypeCommand, Domain.LeaveType>
{
    public Domain.LeaveType ManualMap(UpdateLeaveTypeCommand source)
    {
        return new Domain.LeaveType()
        {
            Id = source.Id,
            Name = source.Name,
            DefaultDays = source.DefaultDays
        };
    }
}
