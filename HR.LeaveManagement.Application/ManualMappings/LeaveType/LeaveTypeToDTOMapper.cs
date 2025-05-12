using HR.LeaveManagement.Application.Features.LeaveType.DTO;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveType;

public class LeaveTypeToDtoMapper : IManualMapper<Domain.LeaveType, LeaveTypeDTO>
{
    public LeaveTypeDTO ManualMap(Domain.LeaveType source)
    {
        return new LeaveTypeDTO
        {
            Id = source.Id,
            Name = source.Name,
            DefaultDays = source.DefaultDays
        };
    }
}
