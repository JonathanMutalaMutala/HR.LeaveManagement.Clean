using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    /// <summary>
    /// Classe representant la requete qui sera envoyé 
    /// Elle permet d'encapsuler les datas
    /// </summary>
    public class CreateleaveTypeCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;

        public int DefaultDays { get; set; } 
    }
}
