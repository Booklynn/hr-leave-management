using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveType;

public class CreateLeaveTypeMapper : IManualMapper<CreateLeaveTypeCommand, Domain.LeaveType>
{
    public Domain.LeaveType ManualMap(CreateLeaveTypeCommand source)
    {
        return new Domain.LeaveType
        {
            Name = source.Name,
            DefaultDays = source.DefaultDays
        };
    }
}
