using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Index
    {
        [Inject] ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        public AdminLeaveRequestViewVM Model { get; set;}

        protected override async Task OnInitializedAsync()
        {
            Model = await leaveRequestService.GetAdminLeaveRequestList();
        }

        void GoToDetails(int id)
        {
            navigationManager.NavigateTo($"/leaverequests/details/{id}");
        }
    }
}