using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes;

[ValidateAntiForgeryToken]
public class EditModel(IDispatcher dispatcher) : PageModel
{
    [BindProperty]
    public required UpdateLeaveTypeCommand Command { get; set; }

    public LeaveTypeDetailsDTO? LeaveTypeDetails { get; private set; }

    public bool IsFromDetailPage { get; private set; } = false;

    public async Task<IActionResult> OnGetAsync(int id, bool isFromDetailPage = false)
    {
        try
        {
            IsFromDetailPage = isFromDetailPage;
            LeaveTypeDetails = await dispatcher.Send(new GetLeaveTypeDetailsQuery(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
            return Page();

        await dispatcher.Send(new UpdateLeaveTypeCommand(id, Command.Name, Command.DefaultDays));
        return RedirectToPage("Index");
    }
}
