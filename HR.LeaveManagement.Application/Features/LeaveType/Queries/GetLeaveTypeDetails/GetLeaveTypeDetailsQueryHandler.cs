using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler(
    ILeaveTypeRepository leaveTypeRepository, 
    IQueryMapper<Domain.LeaveType, LeaveTypeDetailsDTO> mapper,
    IAppLogger<GetLeaveTypeDetailsQueryHandler> logger) 
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDTO>
{
    public async Task<LeaveTypeDetailsDTO> Handle(GetLeaveTypeDetailsQuery request)
    {
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);
        if (leaveType == null)
        {
            logger.LogWarning("{LeaveType} ({Id}) was not found", nameof(LeaveType), request.Id);
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        logger.LogInformation("{LeaveType} ({Id}) was retrieved successfully", nameof(LeaveType), leaveType.Id);
        return mapper.Map(leaveType);
    }
}
