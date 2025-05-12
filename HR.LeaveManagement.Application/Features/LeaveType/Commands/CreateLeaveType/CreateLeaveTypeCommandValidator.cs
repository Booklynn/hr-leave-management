using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(request => request.DefaultDays)
           .InclusiveBetween(1, 100).WithMessage("{PropertyName} must be between 1 and 100.");

        RuleFor(request => request)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");
    }

    private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
