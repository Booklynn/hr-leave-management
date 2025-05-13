using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Mappings.LeaveType;

public class QueryLeaveTypeMapper : IQueryMapper<Domain.LeaveType, LeaveTypeDTO>
{
    public LeaveTypeDTO Map(Domain.LeaveType source)
    {
        return new LeaveTypeDTO
        {
            Id = source.Id,
            Name = source.Name,
            DefaultDays = source.DefaultDays
        };
    }
}
