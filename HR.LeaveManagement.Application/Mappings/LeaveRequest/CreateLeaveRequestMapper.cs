using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

namespace HR.LeaveManagement.Application.Mappings.LeaveRequest;

public class CreateLeaveRequestMapper : ICreateMapper<CreateLeaveRequestCommand, Domain.LeaveRequest>
{
    public Domain.LeaveRequest Map(CreateLeaveRequestCommand source)
    {
        return new Domain.LeaveRequest
        {
            RequestComments = source.RequestComments,
            LeaveTypeId = source.LeaveTypeId,
            StartDate = source.StartDate,
            EndDate = source.EndDate
        };
    }
}
