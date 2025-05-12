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
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .GreaterThan(0)
            .MustAsync(LeaveTypeExists).WithMessage("Leave type does not exist");

        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters")
            .MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists.");

        RuleFor(request => request.DefaultDays)
           .InclusiveBetween(1, 100).WithMessage("{PropertyName} must be between 1 and 100.");
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
