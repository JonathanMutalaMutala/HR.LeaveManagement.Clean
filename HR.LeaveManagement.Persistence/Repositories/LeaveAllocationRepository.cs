using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
           await _context.AddRangeAsync(allocations);
           await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.leaveAllocations.AnyAsync(x => x.EmployeeId == userId &&
                                                        x.LeaveTypeId == leaveTypeId && 
                                                        x.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _context.leaveAllocations.Where(x => x.EmployeeId == userId)
                .Include(x => x.LeaveType)
                .ToListAsync();

            return leaveAllocations;
           
        }

        public  async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsLst()
        {
            var leaveAllocations = await _context.leaveAllocations
                 .Include(x => x.LeaveType)
                 .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await _context.leaveAllocations
                 .Include(x => x.LeaveType)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _context.leaveAllocations.FirstOrDefaultAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId);
        }
    }
    

}
