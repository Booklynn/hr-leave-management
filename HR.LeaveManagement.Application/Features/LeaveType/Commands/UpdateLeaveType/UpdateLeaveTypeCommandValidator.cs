using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}.")
            .MustAsync(LeaveTypeExists).WithMessage("Leave type does not exist");

        RuleFor(request => request.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than {MaxLength} characters.")
            .MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists.");

        RuleFor(request => request.DefaultDays)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(1, 100).WithMessage("{PropertyName} must be between {From} and {To}.");
    }

    private async Task<bool> LeaveTypeExists(int id, CancellationToken token)
    {
        return await _leaveTypeRepository.DoesLeaveTypeExist(id);
    }

    private async Task<bool> LeaveTypeNameUnique(string name, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(name);
    }
}
