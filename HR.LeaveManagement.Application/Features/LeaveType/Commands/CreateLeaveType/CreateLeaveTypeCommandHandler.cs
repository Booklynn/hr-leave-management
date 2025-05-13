using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.ManualMappings;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(
    ILeaveTypeRepository leaveTypeRepository,
    ICreateMapper<CreateLeaveTypeCommand, Domain.LeaveType> mapper,
    IAppLogger<CreateLeaveTypeCommandHandler> logger)
    : IRequestHandler<CreateLeaveTypeCommand, int>
{
    public async Task<int> Handle(CreateLeaveTypeCommand request)
    {
        var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(LeaveType), request);
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }

        var leaveTypeToCreate = mapper.Map(request);
        await leaveTypeRepository.CreateAsync(leaveTypeToCreate);
        logger.LogInformation("{LeaveType} ({Id}) was created successfully", nameof(LeaveType), leaveTypeToCreate.Id);

        return leaveTypeToCreate.Id;
    }
}
