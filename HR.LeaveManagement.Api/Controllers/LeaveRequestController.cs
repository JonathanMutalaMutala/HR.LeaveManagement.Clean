using HR.LeaveManagement.Application.Features.LeaveAlllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.CancelLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.CreateLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.DeleteLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.UpdateLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Query.GetLeaveRequestDetail;
using HR.LeaveManagement.Application.Features.LeaveRequest.Query.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> GetAllLeaveRequests(bool isLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());

            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailsDto>> GetSingleLeaveRequest(int id)
        {
           var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery {Id = id });

            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateLeaveRequest(CreateLeaveRequestCommand newLeaveRequest)
        {
            var response =  await _mediator.Send(newLeaveRequest);
            return Ok(response);
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async  Task<ActionResult> UpdateLeaveRequest(UpdateLeaveRequestCommand updateLeaveRequestCommand)
        {
            await _mediator.Send(updateLeaveRequestCommand);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CancelLeaveRequest(CancelLeaveRequestCommand cancelLeaveRequestCommand)
        {
            await _mediator.Send(cancelLeaveRequestCommand);
            return NoContent();
        }

        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand changeLeaveRequestApprovalCommand)
        {
            await _mediator.Send(changeLeaveRequestApprovalCommand);
            return NoContent() ;    
        }

    }
}
