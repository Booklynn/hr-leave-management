using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveAllocation;

public class CreateLeaveAllowcationMapper : IManualMapper<CreateLeaveAllocationCommand, Domain.LeaveAllocation>
{
    public Domain.LeaveAllocation ManualMap(CreateLeaveAllocationCommand source)
    {
        return new Domain.LeaveAllocation
        {
            LeaveTypeId = source.LeaveTypeId
        };
    }
}
