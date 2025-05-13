using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.ManualMappings;

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
            logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }

        var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);
        if (leaveType == null)
        {
            logger.LogWarning("{LeaveType} ({Id}) was not found", nameof(LeaveType), request.Id);
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        var leaveTypeToUpdate = mapper.Map(request, leaveType);
        if (leaveTypeToUpdate == null)
        {
            logger.LogWarning("{LeaveType} - Invalid mapping from request to entity", nameof(LeaveType), request);
            throw new InvalidMapRequestToEntity(nameof(LeaveType), request);
        }

        await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
        logger.LogInformation("{LeaveType} ({Id}) was updated successfully", nameof(LeaveType), leaveTypeToUpdate.Id);
        
        return leaveTypeToUpdate.Id;
    }
}
