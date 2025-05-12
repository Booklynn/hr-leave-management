using HR.LeaveManagement.Application.Common;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public record CreateLeaveAllocationCommand(int LeaveTypeId) : IRequest<int>;