using HR.LeaveManagement.Application.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Email
{
    /// <summary>
    /// Interface qui represente l'implementation de SendEmailAsync 
    /// Service permettant d'envoyer un email 
    /// </summary>
    public interface  IEmailSender
    {
        Task<bool> SendEmailAsync(EmailMessage email);
    }
}
