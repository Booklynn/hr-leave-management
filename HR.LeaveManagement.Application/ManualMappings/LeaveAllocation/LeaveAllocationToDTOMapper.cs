using HR.LeaveManagement.Application.Features.LeaveAllocation.DTO;
using HR.LeaveManagement.Application.Features.LeaveType.DTO;

namespace HR.LeaveManagement.Application.ManualMappings.LeaveAllocation;

public class LeaveAllocationToDTOMapper : IManualMapper<Domain.LeaveAllocation, LeaveAllocationDTO>
{
    public LeaveAllocationDTO ManualMap(Domain.LeaveAllocation source)
    {
        return new LeaveAllocationDTO
        {
            Id = source.Id,
            NumberOfDays = source.NumberOfDays,
            LeaveType = source.LeaveType != null ? MapLeaveTypeToDTO(source.LeaveType) : null,
            LeaveTypeId = source.LeaveTypeId,
            Period = source.Period,
        };
    }

    private static LeaveTypeDTO MapLeaveTypeToDTO(Domain.LeaveType source)
    {
        return new LeaveTypeDTO
        {
            Id = source.Id,
            Name = source.Name,
            DefaultDays = source.DefaultDays
        };
    }
}
