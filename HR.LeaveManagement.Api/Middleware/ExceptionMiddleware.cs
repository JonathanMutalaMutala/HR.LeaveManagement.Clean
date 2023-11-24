using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;
using System.Net;

namespace HR.LeaveManagement.Api.Middleware
{
    /// <summary>
    /// Ce middleware permet de faire la gestion de tout les exceptions que l'api pourra renvoyé 
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Essayer de passer au HttpContext suivant si on a un Exception on va aller dans le catch 
                await _next(httpContext); 
            }
            catch (Exception ex)
            {
                // Methode permettant de gerer l'excption 
                await HandleExceptionAsync(httpContext,ex);
                
            }
        }

        /// <summary>
        /// Cette classe permet de gerer les Exceptions 
        /// Permet de catch L'exception puis met les informations dans body de la response de Http 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            // Recupere le status code de l'erreur 
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            // Class permettant de mettre les errors dans contient un dictionnaire
            CustomProblemDetails problem = new();
            switch (ex)
            {
                // Au cas ou on a une Bad Request
                // Si la données recus n'est pas bon 
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;

                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        ErrorsDictionnary = badRequestException.ValidationErrors
                    };
                    break;
                // Permet de gerer une NotFoundException 
                // Au cas ou on a une donnée qui n'est pas trouvé
                case NotFoundException NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Detail = NotFound.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;
                    //Autre Exception qu'on peut trouvé dans le Middleware 
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode, 
                        Detail = ex.StackTrace, // avoir le Trace 
                        Type = nameof(HttpStatusCode.InternalServerError) // Type d'exception 
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;

            // Ecrire dans le body de la response 
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
