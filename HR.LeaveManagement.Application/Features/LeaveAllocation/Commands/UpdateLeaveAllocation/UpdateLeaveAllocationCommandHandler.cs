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
            logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveAllocation), request);
            throw new BadRequestException("Invalid LeaveAllocation request", validationResult);
        }

        var leaveAllowcation = await leaveAllocationRepository.GetByIdAsync(request.Id);
        if (leaveAllowcation == null)
        {
            logger.LogWarning("{LeaveAllocation} ({Id}) was not found", nameof(LeaveAllocation), request.Id);
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        var leaveAllowcationToUpdate = mapper.Map(request, leaveAllowcation);
        if (leaveAllowcationToUpdate == null)
        {
            logger.LogWarning("{LeaveAllocation} - Invalid mapping from request to entity", nameof(LeaveAllocation), request);
            throw new InvalidMapRequestToEntity(nameof(LeaveAllocation), request);
        }

        await leaveAllocationRepository.UpdateAsync(leaveAllowcationToUpdate);

        return leaveAllowcation.Id;
    }
}
