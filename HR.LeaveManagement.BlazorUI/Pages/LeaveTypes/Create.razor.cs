using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Create
    {
        [Inject] 
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ILeaveTypeService LeaveTypeService { get; set; }

        [Inject]
        IToastService toastService { get; set; }    

        LeaveTypeVM LeaveTypeVM = new LeaveTypeVM();
        public string Message { get; set; }

        async Task CreateLeaveType()
        {
            var response = await LeaveTypeService.CreateLeaveType(LeaveTypeVM);

            if (response.Success)
            {
                toastService.ShowSuccess("Leave type Created");
                NavigationManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
        

    }
}