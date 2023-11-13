using AutoMapper;
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

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.ChangeLeaveRequestApproval
{
    public  class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand,Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveRequestRepository _leaveRequestRepository; 
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;

        public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, IEmailSender emailSender, 
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _appLogger = appLogger;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if(leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest),request.Id);
            }

            leaveRequest.Approved = request.Approved;

            await _leaveRequestRepository.UpdateAsync(leaveRequest);



            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D}" +
                      $"has been updated successfully.",
                    Subject = "Leave Request approval status Updated"
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
