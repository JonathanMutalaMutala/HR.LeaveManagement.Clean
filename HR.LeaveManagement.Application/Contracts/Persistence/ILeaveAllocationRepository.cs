using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    /// <summary>
    /// Interface qui definit les methodes particulier pour l'entité LeaveLocation 
    /// </summary>
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        /// <summary>
        /// Permet de récupérer une Allocation avec les details en passant son ID
        /// </summary>
        /// <param name="id">Represente l'id de L'allocation </param>
        /// <returns>Un objet LeaveAllocation si cette dernier correspond</returns>
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);

        /// <summary>
        /// Methode permettant de retourner toutes les allocations avec les details 
        /// </summary>
        /// <returns>Une liste d'allocation</returns>
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsLst();

        /// <summary>
        /// Methode permettant de Recuperer la liste des Allocations d'un employé en passant L'id de l'employé comme parametre 
        /// </summary>
        /// <param name="userId">Represente L'id de l'employé</param>
        /// <returns>Une liste d'employé</returns>
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
        /// <summary>
        /// Méthode permettant de verifier si une allocation existe 
        /// Verifie avec si l'allocation appartient au user et le type correspont et la period
        /// </summary>
        /// <param name="userId">Represente l'id de l'employé </param>
        /// <param name="leaveTypeId">Represente Le LeaveType</param>
        /// <param name="period">Represente la periode</param>
        /// <returns>True si L'allocation existe</returns>
        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        /// <summary>
        /// Methode permettant d'ajouter des allocations 
        /// </summary>
        /// <param name="allocations">Represente la liste d'allocations</param>
        /// <returns>un status 200</returns>
        Task AddAllocations(List<LeaveAllocation> allocations); 

        /// <summary>
        /// Methode permettant de recuperer L'allocation d'un user en passant L'id de l'utilisateur et le leaveType
        /// </summary>
        /// <param name="userId">Represente L'id de l'utilisateur</param>
        /// <param name="leaveTypeId">Represente L'id du LeaveType</param>
        /// <returns>une allocation</returns>
        Task<LeaveAllocation> GetUserAllocations(string userId,int leaveTypeId);
    }


}
