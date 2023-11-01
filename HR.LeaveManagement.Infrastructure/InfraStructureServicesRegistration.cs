﻿using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Infrastructure.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfraStructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>(); // AddTransient parce qu'on veut une nouvelle instance à chaque fois qu'on veut utiliser 


            return services;
        }
    }
}
