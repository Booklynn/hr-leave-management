using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public record CreateLeaveRequestCommand(string RequestComments) : BaseLeaveRequest, IRequest<int>;
