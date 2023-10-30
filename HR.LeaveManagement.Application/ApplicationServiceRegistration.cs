using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application
{
    /// <summary>
    /// Classe permettant de faire l'injection de dépendance dans l'application 
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Méthode d'extension qui prend un objet IserviceCollection 
        /// </summary>
        /// <param name="services">Service qu'on souhaite injecter </param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Ajout du service autoMapper à la collection des services 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Ajout du services MediatR dans la collection de services 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
