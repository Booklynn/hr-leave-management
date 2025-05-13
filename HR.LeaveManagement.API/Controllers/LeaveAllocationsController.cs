using HR.LeaveManagement.Application.Common;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.DTOs;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetListAllocations;
using Microsoft.AspNetCore.Mvc;


namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController(IDispatcher dispatcher) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LeaveAllocationDTO>>> Get()
        {
            return Ok(await dispatcher.Send(new GetLeaveAllocationsQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeaveAllocationDetailsDTO>> Get([FromRoute] int id)
        {
            return Ok(await dispatcher.Send(new GetLeaveAllocationDetailsQuery(id)));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand request)
        {
            var response = await dispatcher.Send(request);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateLeaveAllocationCommand request)
        {
            await dispatcher.Send(new UpdateLeaveAllocationCommand(id, request.NumberOfDays, request.LeaveTypeId, request.Period));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await dispatcher.Send(new DeleteLeaveAllocationCommand(id));
            return NoContent();
        }
    }
}
