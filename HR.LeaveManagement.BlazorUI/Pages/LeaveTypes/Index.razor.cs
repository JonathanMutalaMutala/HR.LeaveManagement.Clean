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


        #endregion

        #region Properties 
        public string Message { get; set; } = string.Empty;
        public List<LeaveTypeVM> leaveTypeVMs { get; set; }

        #endregion 
        
        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");
        }

        public void AllocateLeaveType(int id)
        {

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
                await InvokeAsync(StateHasChanged);
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