using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<LeaveRequestDetailsDTO>;
