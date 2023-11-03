using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    /// <summary>
    /// Represente le Handler de la methode 
    /// </summary>
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateleaveTypeCommand, int>
    {
        /*Injection de dependances */
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;


        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        /// <summary>
        /// Methode qui va Handle la request Commande 
        /// Cette methode Verifie les proprieté  envoyés en fonction de la logique metier mis en place
        /// Verifie si les proprietés sont valides 
        /// Convertis les Dto Domain entity Object 
        /// Crée un New LeaveType
        /// Retourne L'id du LeaveType
        /// </summary>
        /// <param name="request">Represente l'object envoyée</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public  async Task<int> Handle(CreateleaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeCommandValidator(this._leaveTypeRepository); // Creation du constructeur pour la validation des Property 

            var validationResult = await validator.ValidateAsync(request); // Appel de la methode qui permet de faire la validation des elements 


            // SI la validation n'est pas valide 
            if (!validationResult.IsValid)
            {
                // Envoie de l'exception 
                throw new BadRequestException("Invalid LeaveType", validationResult); // Permet de faire la validation en appellant le constructeur de la BadRequestExcepetion 
            }

            //Convertir to domain entity object 
            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);


            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);


            return leaveTypeToCreate.Id;

        }
    }
}
