using HR.LeaveManagement.BlazorUI.Models.LeaveAllocation;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;

namespace HR.LeaveManagement.BlazorUI.Models.Employee
{
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationVM> leaveAllocationVMs { get; set; } = new List<LeaveAllocationVM>();

        public List<LeaveRequestVM> leaveRequestVMs { get; set;} = new List<LeaveRequestVM>();
    }
}
