using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllowcationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllowcationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(request => request.LeaveTypeId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .GreaterThan(0)
            .MustAsync(LeaveTypeExists)
            .WithMessage("Leave type does not exist");
    }

    private async Task<bool> LeaveTypeExists(int id, CancellationToken token)
    {
        return await _leaveTypeRepository.DoesLeaveTypeExist(id);
    }
}
