using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllocationDetails;

public record GetLeaveAllocationDetailsQuery(int Id) : IRequest<LeaveAllocationDetailsDTO>;
