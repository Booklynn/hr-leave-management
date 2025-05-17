using HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Mappings.LeaveRequest;

public class QueryLeaveRequestMapper(IQueryMapper<Domain.LeaveType, LeaveTypeDTO> leaveTypeMapper) : IQueryMapper<Domain.LeaveRequest, LeaveRequestDTO>
{
    public LeaveRequestDTO Map(Domain.LeaveRequest source)
    {
        return new LeaveRequestDTO
        {
            RequestingEmployeeId = source.RequestingEmployeeId,
            LeaveType = source.LeaveType != null ? leaveTypeMapper.Map(source.LeaveType) : null,
            DateRequested = source.DateRequested,
            StartDate = source.StartDate,
            EndDate = source.EndDate,
            Approved = source.Approved,
        };
    }
}
