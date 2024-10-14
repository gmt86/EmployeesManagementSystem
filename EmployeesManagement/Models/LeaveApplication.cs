using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.Models
{
    public class LeaveApplication: ApprovedActivity
    {
        public int Id { get; set; }
        [Display(Name="Employe")]
        public int EmployeId { get; set; }
        public Employe Employe { get; set; }

        [Display(Name = "Nber Day")]
        public int NoOfDays { get; set; }  
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Display(Name = "Duration")]
        public int DurationId { get; set; }
        public SystemCodeDetail Duration { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public string? Attachement { get; set; }
        [Display(Name = "Note")]
        public string Description { get; set; }
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }

        [Display(Name = "Approval Note")]
        public string ApprovalNote { get; set; }
    }
}
