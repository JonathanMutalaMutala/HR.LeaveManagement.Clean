using FluentValidation;
using FluentValidation.Validators;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAlllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;

            // Le nombre de jour doit etre superieur à zero
            RuleFor(p => p.NumberOfDays)
                  .GreaterThan(0).WithMessage("{Property} must greater than {ComparisonValue}");

            //La period doit etre plus grand ou egale à la date actuelle 
            RuleFor(p => p.Period)
               .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

            //La leaveType doit exister 
            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");

            //LeaveAllocation Doit exister 
            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveAllocationMustExist)
                .WithMessage("{PropertyName} must be present");
        }
        private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken arg2)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
