using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        public LeaveTypeService(IClient client, IMapper mapper) : base(client, mapper)
        {
        }

        public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
        {
            try
            {
                var createLeaveTypeCommand = _mapper.Map<CreateleaveTypeCommand>(leaveType);
                await _client.LeaveTypesPOSTAsync(createLeaveTypeCommand);
                return new Response<Guid>
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiException<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiException<Guid>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
           var leaveType = await _client.LeaveTypesGETAsync(id); 
           return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        /// <summary>
        /// Cette methode Recupere tout les LeaveTypes 
        /// Puis Renvoie La liste de LeaveTypeVM en utilisant AutoMapper Pour mapper les Datas
        /// </summary>
        /// <returns></returns>
        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await _client.LeaveTypesAllAsync(); // Recuperation de la liste de LeaveType
            
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes); // Retourn La liste de LeaveTypeVM en Mappant aussi Avec le DTo que L'api retourne 

        }

        public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypesPUTAsync(id.ToString(),updateLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiException<Guid>(ex); 
            }


        }
    }

}
