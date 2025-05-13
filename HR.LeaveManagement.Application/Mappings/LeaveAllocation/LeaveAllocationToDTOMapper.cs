using HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Mappings.LeaveAllocation;

public class LeaveAllocationToDTOMapper : IQueryMapper<Domain.LeaveAllocation, LeaveAllocationDTO>
{
    public LeaveAllocationDTO Map(Domain.LeaveAllocation source)
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
