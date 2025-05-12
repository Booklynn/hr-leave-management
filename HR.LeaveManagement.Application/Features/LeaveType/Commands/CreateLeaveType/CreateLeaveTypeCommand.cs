using HR.LeaveManagement.Application.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public record CreateLeaveTypeCommand(string Name, int DefaultDays) : IRequest<int>;
