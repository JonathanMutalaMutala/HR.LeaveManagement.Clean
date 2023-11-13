using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.CreateLeaveRequestCommand;
using HR.LeaveManagement.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.CancelLeaveRequestCommand
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand,Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if(leaveRequest is null)
            {
                throw new NotFoundException(nameof(leaveRequest),request.Id);
            }

            leaveRequest.Cancelled = true;

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D}" +
                      $"has been updated successfully.",
                    Subject = "Leave Request Cancelled"
                };
                await _emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {

                _appLogger.LogWarning(ex.Message);
            }


            return Unit.Value;
        }
    }
}
