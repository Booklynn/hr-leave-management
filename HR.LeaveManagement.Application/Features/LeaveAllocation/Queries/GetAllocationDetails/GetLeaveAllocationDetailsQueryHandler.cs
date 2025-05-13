using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler(ILeaveAllocationRepository repository, 
    IQueryMapper<Domain.LeaveAllocation, LeaveAllocationDetailsDTO> mapper, 
    IAppLogger<GetLeaveAllocationDetailsQueryHandler> logger) 
    : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDTO>
{
    public async Task<LeaveAllocationDetailsDTO> Handle(GetLeaveAllocationDetailsQuery request)
    {
        var leaveAllocation = await repository.GetLeaveAllocationWithDetails(request.Id);
        if (leaveAllocation == null)
        {
            logger.LogWarning("{LeaveType} ({Id}) was not found", nameof(LeaveAllocation), request.Id);
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        logger.LogInformation("{LeaveType} ({Id}) was retrieved successfully", nameof(LeaveAllocation), leaveAllocation.Id);
        return mapper.Map(leaveAllocation);
    }
}
