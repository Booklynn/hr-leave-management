using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetListLeaveRequests;

public class GetLeaveRequestsQueryHandler(
    ILeaveRequestRepository leaveRequestRepository,
    IQueryMapper<Domain.LeaveRequest, LeaveRequestDTO> mapper,
    IAppLogger<GetLeaveRequestsQueryHandler> logger) : IRequestHandler<GetLeaveRequestsQuery, IReadOnlyList<LeaveRequestDTO>>
{
    public async Task<IReadOnlyList<LeaveRequestDTO>> Handle(GetLeaveRequestsQuery request)
    {
        var leaveRequests = await leaveRequestRepository.GetListLeaveRequestsWithDetails();
        return [.. mapper.MapMany(leaveRequests)];
    }
}
