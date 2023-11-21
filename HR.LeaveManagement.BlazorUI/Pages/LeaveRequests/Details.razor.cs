using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Details
    {
        [Inject] ILeaveRequestService leaveRequestService {  get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        [Parameter] public int id { get; set; }

        string ClassName;
        string HeadingText;

        public LeaveRequestVM Model { get; private set; } = new LeaveRequestVM();


        protected override async Task OnParametersSetAsync()
        {
            Model = await leaveRequestService.GetLeaveRequestById(id);
        }

        protected override async Task OnInitializedAsync()
        {
            if (Model.Approved == null)
            {
                ClassName = "warning";
                HeadingText = "Pending Approval";
            }
            else if(Model.Approved == true)
            {
                ClassName = "success";
                HeadingText = "Approved";
            }else
            {
                ClassName = "danger";
                HeadingText = "Rejected";
            }
        }
        async Task ChangeApproval(bool approveStatus)
        {
           await leaveRequestService.ApproveLeaveRequest(id, approveStatus);
            navigationManager.NavigateTo("/leaverequests/");
        }
    }
}