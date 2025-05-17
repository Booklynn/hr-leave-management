using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler(
    ILeaveRequestRepository leaveRequestRepository,
    IQueryMapper<Domain.LeaveRequest, LeaveRequestDetailsDTO> mapper,
    IAppLogger<GetLeaveRequestDetailsQueryHandler> logger) 
    : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDTO>
{
    public async Task<LeaveRequestDetailsDTO> Handle(GetLeaveRequestDetailsQuery request)
    {
        var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        if (leaveRequest == null)
        {
            logger.LogWarning("{@LeaveRequest} - {@Id} was not found", nameof(LeaveRequest), request.Id);
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        return mapper.Map(leaveRequest);
    }
}
