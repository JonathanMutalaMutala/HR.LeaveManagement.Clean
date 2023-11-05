using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAlllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        /// <summary>
        /// Cettte methode permet de mettre à jour une LeaveAllocation 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                throw new BadRequestException("Invalid Leave Allocation", validatorResult);
            }

            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            if(leaveAllocation == null)
            {
                throw new NotFoundException(nameof(leaveAllocation), request.Id);
            }

            _mapper.Map(request,leaveAllocation);

            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return Unit.Value;

        }
    }
}
