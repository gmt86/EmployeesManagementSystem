namespace EmployeesManagement.Models
{
    public class ApprovedActivity :UserActivity
    {
        public string? ApprovedById { get; set; }
        public DateTime? ApprovedOn { get; set; }               
    }
}
