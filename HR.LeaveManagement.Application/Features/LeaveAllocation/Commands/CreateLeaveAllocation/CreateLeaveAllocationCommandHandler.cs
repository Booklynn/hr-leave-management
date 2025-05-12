using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.ManualMappings;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    IManualMapper<CreateLeaveAllocationCommand, Domain.LeaveAllocation> mapper,
    IAppLogger<CreateLeaveAllocationCommandHandler> logger) 
    : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    public async Task<int> Handle(CreateLeaveAllocationCommand request)
    {
        var validator = new CreateLeaveAllowcationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(LeaveAllocation), request);
            throw new BadRequestException("Invalid LeaveAllocation request", validationResult);
        }

        var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
        
        
        var leaveAllocation = mapper.ManualMap(request);


        await leaveAllocationRepository.CreateAsync(leaveAllocation);


        return leaveAllocation.Id;
    }
}
