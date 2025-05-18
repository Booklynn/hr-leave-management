using FluentValidation;
using HR.LeaveManagement.Application.Contracts.DateAndTime;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateLeaveAllocationCommandValidator(
        ILeaveTypeRepository leaveTypeRepository, 
        ILeaveAllocationRepository leaveAllocationRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _dateTimeProvider = dateTimeProvider;

        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}.");

        RuleFor(request => request.Id)
            .MustAsync(LeaveAllocationExists)
            .WithMessage("Leave type does not exist.")
            .When(request => request.Id > 0);

        RuleFor(request => request.NumberOfDays)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}.");

        RuleFor(request => request.LeaveTypeId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}.")
            .MustAsync(LeaveTypeExists).WithMessage("Leave type does not exist.");

        RuleFor(request => request.LeaveTypeId)
            .MustAsync(LeaveTypeExists).WithMessage("Leave type does not exist.")
            .When(request => request.LeaveTypeId > 0);

        RuleFor(request => request.Period)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(_dateTimeProvider.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}.");
    }

    private async Task<bool> LeaveTypeExists(int id, CancellationToken token)
    {
        return await _leaveTypeRepository.DoesLeaveTypeExist(id);
    }

    private async Task<bool> LeaveAllocationExists(int id, CancellationToken token)
    {
        return await _leaveAllocationRepository.DoesLeaveTypeExist(id);
    }
}
