using Blazored.LocalStorage;

namespace HR.LeaveManagement.BlazorUI.Handlers
{
    /// <summary>
    /// Cette classe permet d'intercepter toutes les requetes Http sortant 
    /// et Ajouter le JWt aux Header d'autorization 
    /// Added By Jonathan Mutala
    /// </summary>
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public JwtAuthorizationMessageHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService; 
        }

        /// <summary>
        /// Cette methode est appelé lorsqu'une requete HTTP est émise. 
        /// Va recuperer le JWT dans le localStorage Puis l'ajouter en-tete d'autorisation "Bearer" à la requete HTTP 
        /// Added By Jonathan Mutala 
        /// </summary>
        /// <param name="request">Represente la requete Http qu'on souhaite envoyer </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>("token");   // Recuperation de jwtToken dans la session storage 

            if(!string.IsNullOrEmpty(token)) // Verifie s'il y un Token 
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);    
        }
    }
}
