using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index 
    {

        #region Injection Services
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILeaveTypeService LeaveTypeService { get; set; }

        [Inject]
        public ILeaveAllocationService LeaveAllocationService { get; set; }

        #endregion

        #region Properties 
        public string Message { get; set; } = string.Empty;
        public List<LeaveTypeVM> leaveTypeVMs { get; set; }

        [Inject]
        IToastService toastService { get; set; }    
        #endregion 
        
        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");
        }

        public void AllocateLeaveType(int leaveTypeId)
        {
            LeaveAllocationService.CreateLeaveAllocations(leaveTypeId);
        }
        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}"); 
        }
        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");
        }
        protected async void DeleteLeaveType(int id)
        {
            //NavigationManager.NavigateTo($"/leavetypes/delete/{id}");
            var response = await LeaveTypeService.DeleteLeaveType(id);

            if (response.Success)
            {
                toastService.ShowSuccess("leave type deleted Succes");
                 await OnInitializedAsync();
            }else
            {
                Message = response.Message;
            }
        }
        protected override  async Task OnInitializedAsync()
        {
            leaveTypeVMs = await LeaveTypeService.GetLeaveTypes(); // Recuperer La liste de LeaveTypes
        }
    }
}