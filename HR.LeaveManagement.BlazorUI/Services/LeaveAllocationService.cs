using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, mapper, localStorageService)
        {
        }

        /// <summary>
        /// Methode permettant de créer une LeaveAllocation 
        /// </summary>
        /// <param name="leaveTypeId">Represente L'id de la LeaveType</param>
        /// <returns>un status code 200 si crée</returns>
        public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<Guid>();
                CreateLeaveAllocationCommand createLeaveAllocation = new() { LeaveTypeId = leaveTypeId };   

                await _client.LeaveAllocationPOSTAsync(createLeaveAllocation);

                return response;
            }
            catch (ApiException ex)
            {

                return ConvertApiException<Guid>(ex);
            }
        }
    }

}
