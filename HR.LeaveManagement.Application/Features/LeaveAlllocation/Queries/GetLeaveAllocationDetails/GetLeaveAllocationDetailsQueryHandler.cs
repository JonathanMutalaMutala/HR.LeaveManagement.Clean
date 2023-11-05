using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAlllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
    {
        private readonly IMapper _autoMapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationDetailsQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

      
        /// <summary>
        /// Cette methode permet de recuperer Une LeaveAllocation 
        /// En utilisant son ID 
        /// </summary>
        /// <param name="request">Represente l'objet à passer</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

            //Retourne une exception si l'objet n'existe plus dans la base de donnée
            if (leaveAllocation == null)
            {
                throw new NotFoundException(nameof(leaveAllocation),request.Id);
            }

            var data = _autoMapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);

            return data;
          
        }
    }
}
