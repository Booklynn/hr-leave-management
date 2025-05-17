using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Mappings;
using HR.LeaveManagement.Application.Models.Email;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler(
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveRequestRepository leaveRequestRepository,
    ICreateMapper<CreateLeaveRequestCommand, Domain.LeaveRequest> mapper,
    IEmailSender emailSender,
    IAppLogger<CreateLeaveRequestCommandHandler> logger) : IRequestHandler<CreateLeaveRequestCommand, int>
{
    public async Task<int> Handle(CreateLeaveRequestCommand request)
    {
        var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("{@LeaveRequest} - Validation errors while processing create for {@request}", nameof(LeaveRequest), request);
            throw new BadRequestException("Invalid LeaveRequest request", validationResult);
        }

        var leaveReqeust = mapper.Map(request);
        await leaveRequestRepository.CreateAsync(leaveReqeust);

        try
        {
            var email = new EmailMessage()
            {
                To = string.Empty,
                Subject = " Leave Request Submitted",
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been created successfully",
            };

            if (!await emailSender.SendEmail(email))
            {
                logger.LogWarning("{@LeaveRequest} - {@Id} - Failed to send email", nameof(LeaveRequest), leaveReqeust.Id);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return leaveReqeust.Id;
    }
}
