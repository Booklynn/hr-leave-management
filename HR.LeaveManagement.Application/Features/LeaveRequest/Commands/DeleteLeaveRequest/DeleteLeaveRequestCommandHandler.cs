using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler(
    ILeaveRequestRepository leaveRequestRepository, 
    IAppLogger<DeleteLeaveRequestCommandHandler> logger) 
    : IRequestHandler<DeleteLeaveRequestCommand, int>
{
    public async Task<int> Handle(DeleteLeaveRequestCommand request)
    {
        var leaveRequestToDelete = await leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequestToDelete == null)
        {
            logger.LogWarning("{@LeaveRequest} - {@Id} was not found for deleting", nameof(LeaveRequest), request.Id);
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        await leaveRequestRepository.DeleteAsync(leaveRequestToDelete);

        return leaveRequestToDelete.Id;
    }
}
