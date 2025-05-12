using HR.LeaveManagement.Application.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public record DeleteLeaveTypeCommand(int Id) : IRequest<int>;
