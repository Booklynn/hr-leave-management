using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetListAllocations;

public record GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDTO>>;
