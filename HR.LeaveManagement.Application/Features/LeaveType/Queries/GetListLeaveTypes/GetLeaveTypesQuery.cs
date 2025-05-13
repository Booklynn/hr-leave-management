using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetListLeaveTypes;

public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDTO>>;
