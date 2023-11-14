using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _appLogger;
        public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,IAppLogger<GetLeaveTypesQueryHandler> appLogger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._appLogger = appLogger;
        }

        /// <summary>
        /// Cette methode fait trois choses : 
        /// Query la base de donnée,
        /// Convert les Data objects en DTO objects, 
        /// Return la liste de DTO objects
        /// </summary>
        /// <param name="request">Correspond à la query demandé</param>
        /// <param name="cancellationToken"></param>
        /// <returns>La liste des LeaveTypeDto</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            // Recuperation des données au niveau de la base de donnée
            var leaveTypes = await _leaveTypeRepository.GetAsync();


            
            // Convertir les data objects en Dto Objects 
            var data =   _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            _appLogger.LogInformation("Leave type retrouvé avec success");
            // Return data 
            return data;
        }
    }
}
