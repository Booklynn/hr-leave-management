using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Mappings;
using HR.LeaveManagement.Application.Models.Email;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler(
    ILeaveTypeRepository leaveTypeRepository, 
    ILeaveRequestRepository leaveRequestRepository,
    IUpdateMapper<UpdateLeaveRequestCommand, Domain.LeaveRequest> mapper,
    IAppLogger<UpdateLeaveRequestCommandHandler> logger,
    IEmailSender emailSender) 
    : IRequestHandler<UpdateLeaveRequestCommand, int>
{
    public async Task<int> Handle(UpdateLeaveRequestCommand request)
    {
        var validator = new UpdateLeaveRequestCommandValidator(leaveTypeRepository, leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("{@LeaveRequest} - Validation errors while processing update for {@request}", nameof(LeaveRequest), request);
            throw new BadRequestException("Invalid LeaveRequest request", validationResult);
        }

        var leaveRequestToUpdate = await leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequestToUpdate == null)
        {
            logger.LogWarning("{@LeaveRequest} - {@Id} was not found", nameof(LeaveRequest), request.Id);
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        mapper.Map(request, leaveRequestToUpdate);

        await leaveRequestRepository.UpdateAsync(leaveRequestToUpdate);

        try
        {
            var email = new EmailMessage()
            {
                To = string.Empty,
                Subject = "Leave Request Updated",
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been updated successfully",
            };

            if (!await emailSender.SendEmail(email))
            {
                logger.LogWarning("{@LeaveRequest} - {@Id} - Failed to send email", nameof(LeaveRequest), leaveRequestToUpdate.Id);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return leaveRequestToUpdate.Id;
    }
}
