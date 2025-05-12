using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.DTO;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetListLeaveTypes;

public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDTO>>;
