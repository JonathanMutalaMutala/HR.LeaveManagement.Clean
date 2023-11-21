using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.Employee;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class EmployeeIndex
    {
        [Inject] ILeaveRequestService leaveRequestService {  get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        public EmployeeLeaveRequestViewVM Model { get; set;} = new EmployeeLeaveRequestViewVM();

        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Model = await leaveRequestService.GetUserLeaveRequests();
        }
        async Task CancelRequestAsync(int id)
        {
            var response = await leaveRequestService.CancelLeaveRequestById(id);
            if (response.Success)
            {
                await InvokeAsync(StateHasChanged);
            }else
            {
                Message = response.Message;
            }
        }

    }
}