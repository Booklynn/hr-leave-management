using FluentValidation;
using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(request => request.LeaveTypeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}.")
            .MustAsync(LeaveTypeExists).WithMessage("Leave type does not exist");

        RuleFor(request => request.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .LessThan(request => request.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}.");

        RuleFor(request => request.EndDate)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(request => request.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}.");
    }

    private async Task<bool> LeaveTypeExists(int id, CancellationToken token)
    {
        return await _leaveTypeRepository.DoesLeaveTypeExist(id);
    }
}
