using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes;

[Authorize(Roles = "Administrator")]
public class EditModel(IDispatcher dispatcher) : PageModel
{
    [BindProperty]
    public required UpdateLeaveTypeCommand Command { get; set; }

    public LeaveTypeDetailsDTO? LeaveTypeDetails { get; private set; }

    [BindProperty]
    public bool IsFromDetailPage { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, bool isFromDetailPage = false)
    {
        try
        {
            IsFromDetailPage = isFromDetailPage;
            LeaveTypeDetails = await dispatcher.Send(new GetLeaveTypeDetailsQuery(id));
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
            return Page();

        await dispatcher.Send(new UpdateLeaveTypeCommand(id, Command.Name, Command.DefaultDays));
        
        TempData["SuccessMessage"] = $"Leave type \"{Command.Name}\" updated successfully!";
        
        if (IsFromDetailPage)
        {
            return RedirectToPage("/LeaveTypes/Details", new { id });
        }
        return RedirectToPage("Index");
    }
}
