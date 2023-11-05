using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.DeleteLeaveRequestCommand
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand,Unit>
    {

        public readonly ILeaveRequestRepository _leaveRequestRepository;

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            
            if (leaveRequest == null)
            {
                throw new Exceptions.NotFoundException(nameof(LeaveRequest), request.Id);
            }

            await _leaveRequestRepository.DeleteAsync(leaveRequest);

            return Unit.Value;

        }
    }
}
