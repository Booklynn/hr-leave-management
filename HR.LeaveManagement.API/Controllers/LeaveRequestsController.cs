using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.DTOs;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetListLeaveRequests;
using Microsoft.AspNetCore.Mvc;


namespace HR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController(IDispatcher dispatcher) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeaveRequestDTO>>> Get()
    {
        var leaveRequests = await dispatcher.Send(new GetLeaveRequestsQuery());
        return Ok(leaveRequests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDetailsDTO>> Get(int id)
    {
        var leaveRequest = await dispatcher.Send(new GetLeaveRequestDetailsQuery(id));
        return Ok(leaveRequest);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand request)
    {
        var response = await dispatcher.Send(request);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateLeaveRequestCommand request)
    {
        await dispatcher.Send(request);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> CancelRequest([FromBody] CancelLeaveRequestCommand request)
    {
        await dispatcher.Send(request);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateApprovalRequest([FromBody] ChangeLeaveRequestApprovalCommand request)
    {
        await dispatcher.Send(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await dispatcher.Send(new DeleteLeaveRequestCommand(id));
        return NoContent();
    }
}
