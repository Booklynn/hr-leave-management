using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveType;

public class QueryLeaveTypeDetailsMapper : IQueryMapper<Domain.LeaveType, LeaveTypeDetailsDTO>
{
    public LeaveTypeDetailsDTO Map(Domain.LeaveType source)
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
