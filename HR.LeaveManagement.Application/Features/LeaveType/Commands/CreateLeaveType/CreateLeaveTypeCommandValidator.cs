using FluentValidation;
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
        public CreateLeaveTypeCommandValidator() 
        {
            //Permet de faire la validation sur la proprieté Name de LeaveType
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is Required") // Verifie si le property est vide 
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must be fewer than 30 characters"); 

            RuleFor(p => p.DefaultDays)
                .GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .LessThan(1).WithMessage("{PropertyName} Cannot be less than 1");
        }
    }
}
