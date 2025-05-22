using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetListLeaveTypes;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes
{
    public class IndexModel(IAntiforgery antiforgery, IDispatcher dispatcher) : PageModel
    {
        public IReadOnlyList<LeaveTypeDTO> LeaveTypes { get; private set; } = [];

        public async Task OnGetAsync()
        {
            LeaveTypes = await dispatcher.Send(new GetLeaveTypesQuery());
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromBody] DeleteLeaveTypeCommand request)
        {
            try
            {
                await antiforgery.ValidateRequestAsync(HttpContext);
                await dispatcher.Send(new DeleteLeaveTypeCommand(request.Id));
                return new JsonResult(new { success = true });
            }
            catch (AntiforgeryValidationException)
            {
                return new JsonResult(new { success = false, message = "Delete failed." });
            }
            catch (Exception)
            {
                return new JsonResult(new { success = false, message = "Delete failed." });
            }
        }
    }
}
