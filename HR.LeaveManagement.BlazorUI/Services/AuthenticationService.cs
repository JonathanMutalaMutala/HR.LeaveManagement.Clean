using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        protected readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, IMapper mapper, ILocalStorageService localStorageService,AuthenticationStateProvider authenticationStateProvider) : base(client, mapper, localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }


        /// <summary>
        /// Cette methode envoie les identifiant de connection à L'API et appel la methode permettant de marker le user comme authentifier  
        /// Sinon envoie l'Exception envoyer par L'API 
        /// </summary>
        /// <param name="username">Representante le username de l'utilisateur </param>
        /// <param name="password">Represente le mot de passe de l'utilisateur </param>
        /// <returns></returns>
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            bool authenticated = false;


            try
            {
                AuthRequest authenticationRequest = new AuthRequest() { Email = username, Password = password };

                var authenticationResponse = await _client.LoginAsync(authenticationRequest);
                if (!string.IsNullOrEmpty(authenticationResponse.Token))
                {
                    await _localStorage.SetItemAsStringAsync("token", authenticationResponse.Token);

                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();


                    authenticated = true;
                }
                return authenticated;
            }
            catch (Exception)
            {

                return authenticated;
            }
        }

        public async Task Logout()
        {
            // await _localStorage.RemoveItemAsync("token");
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string userName, string password)
        {
            bool register = false;

            RegistrationRequest registrationRequest = new RegistrationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email,
                UserName = userName,
                Password = password
            }; 

            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                register = true;
            }

            return register; 
        }
    }
}
