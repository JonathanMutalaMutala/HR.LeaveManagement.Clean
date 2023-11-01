using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    /// <summary>
    /// Class permettant de faire la validation de proprieté de  LeaveTypeCommand utilisant la librairie FluentValidation 
    /// </summary>
    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateleaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository) 
        {
            //Permet de faire la validation sur la proprieté Name de LeaveType
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is Required") // Verifie si le property est vide 
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must be fewer than 30 characters"); 

            RuleFor(p => p.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} Cannot be less than 1");

            RuleFor(q => q)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type already exists");
            this._leaveTypeRepository = leaveTypeRepository;
        }

        private Task<bool> LeaveTypeNameUnique(CreateleaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
