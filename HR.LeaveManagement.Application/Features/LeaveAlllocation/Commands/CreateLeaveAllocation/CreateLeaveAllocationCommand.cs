using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAlllocation.Commands.CreateLeaveAllocation
{
    /// <summary>
    /// Represente Le type de commande a avoir pour la creation de la LeaveAllocation
    /// </summary>
    public  class CreateLeaveAllocationCommand : IRequest<Unit>
    {
        public int LeaveTypeId { get; set; } 
    }
}
