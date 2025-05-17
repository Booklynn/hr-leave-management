using HR.LeaveManagement.Application.Common;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public record DeleteLeaveRequestCommand(int Id) : IRequest<int>;
