using HR.LeaveManagement.Application.Features.LeaveType.DTO;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveType;

public class LeaveTypeDetailsToDtoMapper : IManualMapper<Domain.LeaveType, LeaveTypeDetailsDTO>
{
    public LeaveTypeDetailsDTO ManualMap(Domain.LeaveType source)
    {
        return new LeaveTypeDetailsDTO
        {
            Id = source.Id,
            Name = source.Name,
            DefaultDays = source.DefaultDays,
            DateCreated = source.DateCreated,
            DateModified = source.DateModified
        };
    }
}
