using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    /// <summary>
    /// Cette classe provient du pattern CQRS Command Query Responsability Segregation 
    /// herite de la IRequestHandler permet de handle les Query envoyer et de renvoyer le Dto qu'il faut 
    /// 
    /// </summary>
    public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
    {
        /// <summary>
        /// Permet de mapper les Data 
        /// </summary>
        private readonly IMapper _autoMapper;

        /// <summary>
        /// Permet de recuperer la methode concerner au type de methode à  retourner 
        /// </summary>
        private readonly ILeaveTypeRepository _leaveTypeRepository; 


        public GetLeaveTypeDetailsQueryHandler(IMapper autoMapper,ILeaveTypeRepository leaveTypeRepository) 
        {
            _autoMapper = autoMapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        /// <summary>
        /// Cette methode permet de recuperer le Details d'un LeaveType, de le convertir en Dto et de retourner 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // Verifie si le Data existe 
            if (leaveType == null)
            {
                throw new NotFoundException(nameof(leaveType), request.Id);
            }

            var data = _autoMapper.Map<LeaveTypeDetailsDto>(leaveType);

            return data;
        }
    }
}
