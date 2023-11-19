using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.BlazorUI.Models.LeaveTypes
{
    /// <summary>
    /// Cette class sert de viewModel pour la leaveType
    /// </summary>
    public class LeaveTypeVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default Number of Days")]
        public int DefaultDays { get; set; }
    }
}
