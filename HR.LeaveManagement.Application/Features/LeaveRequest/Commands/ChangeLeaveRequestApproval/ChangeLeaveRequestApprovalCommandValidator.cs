using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
    public ChangeLeaveRequestApprovalCommandValidator()
    {

        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(request => request.Approved)
            .NotNull().WithMessage("{PropertyName} is required.");
    }
}
