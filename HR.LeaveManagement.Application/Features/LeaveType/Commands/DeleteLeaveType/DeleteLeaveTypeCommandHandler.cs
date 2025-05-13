using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IAppLogger<DeleteLeaveTypeCommandHandler> logger) : IRequestHandler<DeleteLeaveTypeCommand, int>
{
    public async Task<int> Handle(DeleteLeaveTypeCommand request)
    {
        var leaveTypeToDelete = await leaveTypeRepository.GetByIdAsync(request.Id);
        if (leaveTypeToDelete == null)
        {
            logger.LogWarning("{@LeaveType} - {@Id} was not found for deleting", nameof(LeaveType), request.Id);
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        await leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
        logger.LogInformation("{@LeaveType} - {@Id} was deleted successfully", nameof(LeaveType), leaveTypeToDelete.Id);

        return leaveTypeToDelete.Id;
    }
}
