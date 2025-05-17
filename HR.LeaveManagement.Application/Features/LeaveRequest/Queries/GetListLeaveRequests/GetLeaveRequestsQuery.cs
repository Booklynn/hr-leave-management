using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetListLeaveRequests;

public record GetLeaveRequestsQuery : IRequest<IReadOnlyList<LeaveRequestDTO>>;
