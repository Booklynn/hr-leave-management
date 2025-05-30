﻿using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest?> GetLeaveRequestWithDetails(int id);
    Task<IReadOnlyList<LeaveRequest>> GetListLeaveRequestsWithDetails();
    Task<IReadOnlyList<LeaveRequest>> GetListLeaveRequestsWithDetails(string userId);
}
