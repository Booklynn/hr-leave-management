using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

namespace HR.LeaveManagement.Application.Mappings.LeaveRequest;

public class UpdateLeaveRequestMapper : IUpdateMapper<UpdateLeaveRequestCommand, Domain.LeaveRequest>
{
    public void Map(UpdateLeaveRequestCommand source, Domain.LeaveRequest target)
    {
        target.LeaveTypeId = source.LeaveTypeId;
        target.StartDate = source.StartDate;
        target.EndDate = source.EndDate;
        target.RequestComments = source.RequestComments;
        target.Cancelled = source.Cancelled;
    }
}
