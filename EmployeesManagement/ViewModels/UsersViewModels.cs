using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.ViewModels
{
    public class UsersViewModels
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string? FullName => $"{FirstName} {LastName}";

        [DisplayName("Nationality Id")]
        public string? NationalId { get; set; }

        [DisplayName("User Role")]
        public string? RoleId { get; set; }

    }
}
