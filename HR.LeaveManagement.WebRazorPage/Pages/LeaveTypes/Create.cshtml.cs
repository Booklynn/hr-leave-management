using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes;

[ValidateAntiForgeryToken]
public class CreateModel(IDispatcher dispatcher) : PageModel
{
    [BindProperty]
    public required CreateLeaveTypeCommand Command { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync() 
    {
        if (!ModelState.IsValid)
            return Page();

        await dispatcher.Send(new CreateLeaveTypeCommand(Command.Name, Command.DefaultDays));
        return RedirectToPage("Index");
    }
}
