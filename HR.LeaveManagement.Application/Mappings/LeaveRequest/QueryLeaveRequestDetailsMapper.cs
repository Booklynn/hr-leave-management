using HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Mappings.LeaveRequest;

public class QueryLeaveRequestDetailsMapper(IQueryMapper<Domain.LeaveType, LeaveTypeDTO> leaveTypeMapper) : IQueryMapper<Domain.LeaveRequest, LeaveRequestDetailsDTO>
{
    public LeaveRequestDetailsDTO Map(Domain.LeaveRequest source)
    {
        return new LeaveRequestDetailsDTO
        {
            Approved = source.Approved,
            Cancelled = source.Cancelled,
            DateActioned = source.DateActioned,
            DateRequested = source.DateRequested,
            EndDate = source.EndDate,
            LeaveTypeId = source.LeaveTypeId,
            RequestComments = source.RequestComments,
            RequestingEmployeeId = source.RequestingEmployeeId,
            StartDate = source.StartDate,
            LeaveType = source.LeaveType != null ? leaveTypeMapper.Map(source.LeaveType) : null,
        };
    }
}
