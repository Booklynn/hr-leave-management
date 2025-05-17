using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    
    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}.")
            .MustAsync(LeaveRequestExists).WithMessage("Leave type does not exist.");
    }

    private async Task<bool> LeaveRequestExists(int id, CancellationToken token)
    {
        return await _leaveRequestRepository.GetByIdAsync(id) != null;
    }
}
