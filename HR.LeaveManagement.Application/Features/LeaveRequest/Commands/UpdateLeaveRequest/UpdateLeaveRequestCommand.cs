using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public record UpdateLeaveRequestCommand(int Id, string RequestComments, bool Cancelled) : BaseLeaveRequest, IRequest<int>;
