using HR.LeaveManagement.BlazorWASM.Contracts;
using HR.LeaveManagement.BlazorWASM.Services.Base;

namespace HR.LeaveManagement.BlazorWASM.Services;

public class LeaveAllocationService(IClient client) : BaseHttpService(client), ILeaveAllocationService
{
}
