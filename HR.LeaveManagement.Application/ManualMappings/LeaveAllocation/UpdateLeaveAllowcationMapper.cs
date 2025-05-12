using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveAllocation;

public class UpdateLeaveAllowcationMapper : IManualMapper<UpdateLeaveAllocationCommand, Domain.LeaveAllocation>
{
    public Domain.LeaveAllocation ManualMap(UpdateLeaveAllocationCommand source)
    {
        return new Domain.LeaveAllocation
        {
            Id = source.Id,
            NumberOfDays = source.NumberOfDays,
            LeaveTypeId = source.LeaveTypeId,
            Period = source.Period
        };
    }
}
