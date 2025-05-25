using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes;

[Authorize(Roles = "Administrator")]
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

        try
        {
            await dispatcher.Send(new CreateLeaveTypeCommand(Command.Name, Command.DefaultDays));
            
            TempData["SuccessMessage"] = $"Leave type \"{Command.Name}\" created successfully!";
            
            return RedirectToPage("Index");
        }
        catch (BadRequestException ex)
        {
            var allErrors = ex.ValidationErrors.SelectMany(kv => kv.Value.Select(msg => $"{msg}"));
            
            TempData["ErrorMessage"] = string.Join("<br>", allErrors);
            
            return Page();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Something went wrong.";
            return Page();
        }
    }
}
