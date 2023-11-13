using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestCommand : BaseLeaveRequest,IRequest<Unit>
    {
        public string RequestsComments { get; set; } = string.Empty;
    }
}
