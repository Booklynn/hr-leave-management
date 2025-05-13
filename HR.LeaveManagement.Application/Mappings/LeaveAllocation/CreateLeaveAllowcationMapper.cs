using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

namespace HR.LeaveManagement.Application.Mappings.LeaveAllocation;

public class CreateLeaveAllowcationMapper : ICreateMapper<CreateLeaveAllocationCommand, Domain.LeaveAllocation>
{
    public Domain.LeaveAllocation Map(CreateLeaveAllocationCommand source)
    {
        return new Domain.LeaveAllocation
        {
            LeaveTypeId = source.LeaveTypeId
        };
    }
}
