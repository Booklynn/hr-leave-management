using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

namespace HR.LeaveManagement.Application.Mappings.LeaveRequest;

public class ChangeLeaveRequestApprovalMapper : IUpdateMapper<ChangeLeaveRequestApprovalCommand, Domain.LeaveRequest>
{
    public void Map(ChangeLeaveRequestApprovalCommand source, Domain.LeaveRequest target)
    {
        target.Approved = source.Approved;
    }
}
