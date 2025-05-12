using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.DTO;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDTO>;
