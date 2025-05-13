using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    ICreateMapper<CreateLeaveAllocationCommand, Domain.LeaveAllocation> mapper,
    IAppLogger<CreateLeaveAllocationCommandHandler> logger) 
    : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    public async Task<int> Handle(CreateLeaveAllocationCommand request)
    {
        var validator = new CreateLeaveAllowcationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("{@LeaveAllocation} - Validation errors while processing create for {@request}", nameof(LeaveAllocation), request);
            throw new BadRequestException("Invalid LeaveAllocation request", validationResult);
        }

        var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
        
        
        var leaveAllocation = mapper.Map(request);


        await leaveAllocationRepository.CreateAsync(leaveAllocation);


        return leaveAllocation.Id;
    }
}
