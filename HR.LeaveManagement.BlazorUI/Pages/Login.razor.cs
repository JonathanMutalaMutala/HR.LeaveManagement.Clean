using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages
{
    public partial class Login
    {
        public LoginVM Model { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        protected override void OnInitialized()
        {
            Model = new LoginVM();
        }

        protected async Task HandleLogin()
        {
            if (await AuthenticationService.AuthenticateAsync(Model.Email,Model.Password))
            {
                NavigationManager.NavigateTo("/");
            }
            Message = "UserName/Password combination unknown";
        }
    }
}