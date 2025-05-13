using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.DateAndTime;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler(
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    IUpdateMapper<UpdateLeaveAllocationCommand, Domain.LeaveAllocation> mapper,
    IAppLogger<UpdateLeaveAllocationCommandHandler> logger,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<UpdateLeaveAllocationCommand, int>
{
    public async Task<int> Handle(UpdateLeaveAllocationCommand request)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(leaveTypeRepository, leaveAllocationRepository, dateTimeProvider);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("{@LeaveAllocation} - Validation errors while processing update for {@request}", nameof(LeaveAllocation), request);
            throw new BadRequestException("Invalid LeaveAllocation request", validationResult);
        }

        var leaveAllowcationToUpdate = await leaveAllocationRepository.GetByIdAsync(request.Id);
        if (leaveAllowcationToUpdate == null)
        {
            logger.LogWarning("{@LeaveAllocation} - {@Id} was not found", nameof(LeaveAllocation), request.Id);
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        mapper.Map(request, leaveAllowcationToUpdate);

        await leaveAllocationRepository.UpdateAsync(leaveAllowcationToUpdate);
        logger.LogInformation("{@LeaveAllocation} - @Id was created successfully", nameof(LeaveAllocation), leaveAllowcationToUpdate.Id);

        return leaveAllowcationToUpdate.Id;
    }
}
