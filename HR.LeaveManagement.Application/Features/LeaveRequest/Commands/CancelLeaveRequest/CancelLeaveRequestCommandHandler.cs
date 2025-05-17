using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler(
    ILeaveRequestRepository leaveRequestRepository, 
    IEmailSender emailSender,
    IAppLogger<CancelLeaveRequestCommandHandler> logger) 
    : IRequestHandler<CancelLeaveRequestCommand, int>
{
    public async Task<int> Handle(CancelLeaveRequestCommand request)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest == null)
        {
            logger.LogWarning("{@LeaveRequest} - {@Id} was not found for deleting", nameof(LeaveRequest), request.Id);
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequest.Cancelled = true;

        // TODO: -

        try
        {
            var email = new EmailMessage()
            {
                To = string.Empty,
                Subject = "Leave Request Submitted",
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully",
            };

            if (!await emailSender.SendEmail(email))
            {
                logger.LogWarning("{@LeaveRequest} - {@Id} - Failed to send email", nameof(LeaveRequest), leaveRequest.Id);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return leaveRequest.Id;
    }
}
