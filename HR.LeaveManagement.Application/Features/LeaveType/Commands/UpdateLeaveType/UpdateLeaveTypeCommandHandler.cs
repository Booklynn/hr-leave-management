using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Mappings;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler(
    ILeaveTypeRepository leaveTypeRepository,
    IUpdateMapper<UpdateLeaveTypeCommand, Domain.LeaveType> mapper,
    IAppLogger<UpdateLeaveTypeCommandHandler> logger) 
    : IRequestHandler<UpdateLeaveTypeCommand, int>
{
    public async Task<int> Handle(UpdateLeaveTypeCommand request)
    {
        var validator = new UpdateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("{@LeaveType} - Validation errors while processing request for {@request}", nameof(LeaveType), request);
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }

        var leaveTypeToUpdate = await leaveTypeRepository.GetByIdAsync(request.Id);
        if (leaveTypeToUpdate == null)
        {
            logger.LogWarning("{@LeaveType} - {@Id} was not found", nameof(LeaveType), request.Id);
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        mapper.Map(request, leaveTypeToUpdate);

        await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
        logger.LogInformation("{@LeaveType} - {@Id} was updated successfully", nameof(LeaveType), leaveTypeToUpdate.Id);

        return leaveTypeToUpdate.Id;
    }
}
