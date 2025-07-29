using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.EmployeeManagementSystem
{
    public class SignInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public EmployeeAuthDto Employee { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class EmployeeAuthDto
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffCardID { get; set; }
        public string DepartmentName { get; set; }
        public string OccupationName { get; set; }
        public DateTime LastLogin { get; set; }
    }
}