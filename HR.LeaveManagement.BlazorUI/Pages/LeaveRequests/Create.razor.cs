using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject] 
        private ILeaveTypeService  leaveTypeService { get; set; }

        [Inject]
        private ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager NavigationManager {  get; set; } 

        LeaveRequestVM LeaveRequest {  get; set; } = new LeaveRequestVM();
        List<LeaveTypeVM> LeaveTypeLst { get; set; } = new List<LeaveTypeVM>();

        protected override async Task OnInitializedAsync()
        {
            LeaveTypeLst = await leaveTypeService.GetLeaveTypes();
        }

        private async void HandleValidSubmit()
        {
            await leaveRequestService.CreateLeaveRequest(LeaveRequest);
            NavigationManager.NavigateTo("/leaverequests");
        }
    }
}