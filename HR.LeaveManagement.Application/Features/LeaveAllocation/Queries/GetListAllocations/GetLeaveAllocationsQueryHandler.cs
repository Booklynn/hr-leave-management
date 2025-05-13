using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetListAllocations;

public class GetLeaveAllocationsQueryHandler(
    ILeaveAllocationRepository leaveAllocationRepository, 
    IQueryMapper<Domain.LeaveAllocation, LeaveAllocationDTO> mapper,
    IAppLogger<GetLeaveAllocationsQueryHandler> logger) 
    : IRequestHandler<GetLeaveAllocationsQuery, IReadOnlyList<LeaveAllocationDTO>>
{
    public async Task<IReadOnlyList<LeaveAllocationDTO>> Handle(GetLeaveAllocationsQuery request)
    {
        var leaveAllocations = await leaveAllocationRepository.GetListLeaveAllocationsWithDetails();
        logger.LogInformation("{LeaveAllocation} were retrieved successfully", nameof(LeaveAllocation));
        return [.. mapper.MapMany(leaveAllocations)];
    }
}
