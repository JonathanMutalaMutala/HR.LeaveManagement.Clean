using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveTypeService
    {
        /// <summary>
        /// Methode permettant de recuperer toutes les LeaveTypes
        /// </summary>
        /// <returns></returns>
        Task<List<LeaveTypeVM>> GetLeaveTypes();

        /// <summary>
        /// Methode permettant de recuperer une leaveType en passant un Id en paramettre 
        /// </summary>
        /// <param name="id">Represente L'ID de la LeaveType</param>
        /// <returns></returns>
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);

        Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType);

        Task<Response<Guid>> UpdateLeaveType(int id,LeaveTypeVM leaveType);

        Task<Response<Guid>> DeleteLeaveType(int id);
    }
}
