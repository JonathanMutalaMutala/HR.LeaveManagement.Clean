using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAlllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository,IUserService userService)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            this._userService = userService;
        }

        /// <summary>
        /// Cette methode permet de créer une LeaveAllocation 
        /// Une LeaveAllocation est crée si est seulement si Une LeaveType Existe 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);   

            
            if(!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
            }

            // Recuperer la leaveType de l'allocation 
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            var employees = await _userService.GetEmployeesAsync();

            var allocationsLst = new List<Domain.LeaveAllocation>();

            var period = DateTime.Now.Year; 

            foreach (var emp in employees)
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExists(emp.Id, request.LeaveTypeId, period);

                if (allocationExists == false)
                {
                    allocationsLst.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period
                    });
                }

            }

            // On ajoute seulement si une allocationLeave a été ajouté
            if(allocationsLst.Count > 0)
            {
                await _leaveAllocationRepository.AddAllocations(allocationsLst);
            }

            return Unit.Value;
        }
    }
}
