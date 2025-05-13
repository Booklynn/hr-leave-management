using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

namespace HR.LeaveManagement.Application.Mappings.LeaveAllocation;

public class UpdateLeaveAllowcationMapper : IUpdateMapper<UpdateLeaveAllocationCommand, Domain.LeaveAllocation>
{
    public Domain.LeaveAllocation Map(UpdateLeaveAllocationCommand dto, Domain.LeaveAllocation existingEntity)
    {
        existingEntity.NumberOfDays = dto.NumberOfDays;
        existingEntity.LeaveTypeId = dto.LeaveTypeId;
        existingEntity.Period = dto.Period;
        return existingEntity;
    }
}
