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

        public Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Guid>> DeleteLeaveType(int id)
        {
            throw new NotImplementedException();
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

        public Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            throw new NotImplementedException();
        }
    }

}
