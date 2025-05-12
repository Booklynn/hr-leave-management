using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DTO;
using HR.LeaveManagement.Application.ManualMappings;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetListAllocations;

public class GetLeaveAllocationsQueryHandler(
    ILeaveAllocationRepository leaveAllocationRepository, 
    IManualMapper<Domain.LeaveAllocation, LeaveAllocationDTO> mapper,
    IAppLogger<GetLeaveAllocationsQueryHandler> logger) 
    : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDTO>>
{
    public async Task<List<LeaveAllocationDTO>> Handle(GetLeaveAllocationsQuery request)
    {
        var leaveAllocations = await leaveAllocationRepository.GetListLeaveAllocationsWithDetails();
        logger.LogInformation("{LeaveAllocation} were retrieved successfully", nameof(LeaveAllocation));
        return [.. mapper.ManualMapMany(leaveAllocations)];
    }
}
