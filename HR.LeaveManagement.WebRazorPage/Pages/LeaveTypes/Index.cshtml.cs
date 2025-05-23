using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetListLeaveTypes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes;

public class IndexModel(IDispatcher dispatcher) : PageModel
{
    public IReadOnlyList<LeaveTypeDTO> LeaveTypes { get; private set; } = [];

    public async Task OnGetAsync()
    {
        LeaveTypes = await dispatcher.Send(new GetLeaveTypesQuery());
    }
}
