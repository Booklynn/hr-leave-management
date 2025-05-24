using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler(
    ILeaveTypeRepository leaveTypeRepository, 
    ILeaveRequestRepository leaveRequestRepository,
    IEmailSender emailSender,
    IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger) : IRequestHandler<ChangeLeaveRequestApprovalCommand, int>
{
    public async Task<int> Handle(ChangeLeaveRequestApprovalCommand request)
    {
        var validator = new ChangeLeaveRequestApprovalCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("{@LeaveRequest} - Validation errors while processing update for {@request}", nameof(LeaveRequest), request);
            throw new BadRequestException("Invalid LeaveRequest request", validationResult);
        }

        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest == null)
        {
            logger.LogWarning("{@LeaveRequest} - {@Id} was not found", nameof(LeaveRequest), request.Id);
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequest.Approved = request.Approved;
        await leaveRequestRepository.UpdateAsync(leaveRequest);

        // TODO: -

        try
        {
            var email = new EmailMessage()
            {
                To = string.Empty,
                Subject = "Leave Request Approval Status Updated",
                Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated",
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
