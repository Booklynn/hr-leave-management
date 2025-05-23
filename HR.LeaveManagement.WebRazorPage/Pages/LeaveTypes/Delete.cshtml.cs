using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes;

public class DeleteModel(IDispatcher dispatcher, IAntiforgery antiforgery) : PageModel
{

    public RedirectToPageResult OnGet()
    {
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostAsync([FromBody] DeleteLeaveTypeCommand command)
    {
        try
        {
            await antiforgery.ValidateRequestAsync(HttpContext);
            var result = await dispatcher.Send(command);
            return new JsonResult(new { success = true, id = result });
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
