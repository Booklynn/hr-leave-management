using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetListLeaveTypes;

public class GetLeaveTypesQueryHandler(
    ILeaveTypeRepository leaveTypeRepository,
    IQueryMapper<Domain.LeaveType, LeaveTypeDTO> mapper,
    IAppLogger<GetLeaveTypesQueryHandler> logger) 
    : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDTO>>
{
    public async Task<List<LeaveTypeDTO>> Handle(GetLeaveTypesQuery request)
    {
        var leaveTypes = await leaveTypeRepository.GetListAsync();
        logger.LogInformation("{LeaveType} were retrieved successfully", nameof(LeaveType));
        return [.. mapper.MapMany(leaveTypes)];
    }
}
