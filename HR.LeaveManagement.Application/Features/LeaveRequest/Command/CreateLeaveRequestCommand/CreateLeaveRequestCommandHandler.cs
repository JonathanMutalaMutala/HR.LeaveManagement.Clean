using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.UpdateLeaveRequestCommand;
using HR.LeaveManagement.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.CreateLeaveRequestCommand
{
    /// <summary>
    /// Class permettant de Handler la commande de CreateLeaveRequestCommand 
    /// Cette classe fait appel à la classe CreateLeaveValidator Pour faire la validation des elements ensuite appeler les interfaces pour créer une LeaveRequest
    /// 
    /// </summary>
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand,Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository; 
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

        public CreateLeaveRequestCommandHandler(IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // Appel de la classe pour faire la validation 
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken); // Cette ligne permet d'appeler la methode de validation des property de la command envoyer Si Vrai return un trueè

            //Si non valida on envoie une exception
            if(!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            // Get requesting employee's id

                
            // Check on employee's allocation 

            // if allocations aren't enough return validation error with message



            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);


            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D}" +
                      $"has been updated successfully.",
                    Subject = "Leave Request Créated"
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
