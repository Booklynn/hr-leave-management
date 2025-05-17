using HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Mappings.LeaveAllocation;

public class QueryLeaveAllocationDetailsMapper(IQueryMapper<Domain.LeaveType, LeaveTypeDTO> leaveTypeMapper) : IQueryMapper<Domain.LeaveAllocation, LeaveAllocationDetailsDTO>
{
    public LeaveAllocationDetailsDTO Map(Domain.LeaveAllocation source)
    {
        return new LeaveAllocationDetailsDTO
        {
            Id = source.Id,
            NumberOfDays = source.NumberOfDays,
            LeaveType = source.LeaveType != null ? leaveTypeMapper.Map(source.LeaveType) : null,
            LeaveTypeId = source.LeaveTypeId,
            Period = source.Period
        };
    }
}
