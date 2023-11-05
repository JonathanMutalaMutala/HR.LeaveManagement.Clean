using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAlllocation.Queries.GetLeaveAllocationDetails
{
    public  class GetLeaveAllocationDetailsQuery : IRequest<LeaveAllocationDetailsDto>
    {
        public int Id { get; set; }
    }
}
