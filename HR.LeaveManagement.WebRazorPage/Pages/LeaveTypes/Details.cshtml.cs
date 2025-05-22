using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveType.DTOs;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotFoundException = HR.LeaveManagement.Application.Exceptions.NotFoundException;

namespace HR.LeaveManagement.WebRazorPage.Pages.LeaveTypes
{
    public class DetailsModel(IDispatcher dispatcher) : PageModel
    {
        public LeaveTypeDetailsDTO? LeaveTypeDetails { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                LeaveTypeDetails = await dispatcher.Send(new GetLeaveTypeDetailsQuery(id));
            }
            catch(NotFoundException)
            {
                return NotFound();
            }

            return Page();
        }
    }
}       
