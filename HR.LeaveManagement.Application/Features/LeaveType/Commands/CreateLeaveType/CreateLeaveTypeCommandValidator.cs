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
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than {MaxLength} characters.")
            .MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists.");

        RuleFor(request => request.DefaultDays)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(1, 100).WithMessage("{PropertyName} must be between {From} and {To}.");
    }

    private async Task<bool> LeaveTypeNameUnique(string name, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(name);
    }
}
