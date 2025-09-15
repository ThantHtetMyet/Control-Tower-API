using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.EmployeeManagementSystem
{
    public class CreateEmployeeDto
    {
        [Required]
        public Guid CompanyID { get; set; }

        [Required]
        public Guid SubDepartmentID { get; set; }  // Changed from DepartmentID

        [Required]
        public Guid OccupationID { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffCardID { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffRFIDCardID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string LoginPassword { get; set; }

        [StringLength(200)]
        public string? Remark { get; set; }

        [Required]
        public DateTime StartWorkingDate { get; set; }

        public DateTime? LastWorkingDate { get; set; }

        [StringLength(100)]
        public string? WorkPermit { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; }

        [StringLength(100)]
        public string? Religion { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string? WorkPassCardNumber { get; set; }

        public DateTime? WorkPassCardIssuedDate { get; set; }

        public DateTime? WorkPassCardExpiredDate { get; set; }

        // Emergency Contact Information
        [StringLength(200)]
        public string? EmergencyContactName { get; set; }
    
        public int? EmergencyContactNumber { get; set; }
    
        [StringLength(200)]
        public string? EmergencyRelationship { get; set; }

        public Guid CreatedBy { get; set; }

        // Add application access list
        public List<CreateEmployeeApplicationAccessDto>? ApplicationAccesses { get; set; }
    
        // Add image upload field
        public IFormFile? ProfileImage { get; set; }
    }


    public class UpdateEmployeeDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid CompanyID { get; set; }

        [Required]
        public Guid SubDepartmentID { get; set; }  // Changed from DepartmentID

        [Required]
        public Guid OccupationID { get; set; }

        [Required]
        public string StaffCardID { get; set; }

        [Required]
        public string StaffRFIDCardID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string Gender { get; set; }

        
        public string? Remark { get; set; }

        [Required]
        public DateTime StartWorkingDate { get; set; }

        public DateTime? LastWorkingDate { get; set; }

        [StringLength(100)]
        public string? WorkPermit { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; }

        [StringLength(100)]
        public string? Religion { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string? WorkPassCardNumber { get; set; }

        public DateTime? WorkPassCardIssuedDate { get; set; }

        public DateTime? WorkPassCardExpiredDate { get; set; }

        // Emergency Contact Information
        [StringLength(200)]
        public string? EmergencyContactName { get; set; }
    
        public string? EmergencyContactNumber { get; set; }
    
        [StringLength(200)]
        public string? EmergencyRelationship { get; set; }

        public Guid? UpdatedBy { get; set; }
    
        // Add image upload field
        //public IFormFile? ProfileImage { get; set; }
    }

    public class UserDto
    {
        public Guid ID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid SubDepartmentID { get; set; }  // Changed from DepartmentID
        public Guid OccupationID { get; set; }
        public string StaffCardID { get; set; }
        public string StaffRFIDCardID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string? Remark { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public string? WorkPermit { get; set; }
        public string? Nationality { get; set; }
        public string? Religion { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Work pass card fields
        public string? WorkPassCardNumber { get; set; }
        public DateTime? WorkPassCardIssuedDate { get; set; }
        public DateTime? WorkPassCardExpiredDate { get; set; }

        // Emergency Contact Information
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? EmergencyRelationship { get; set; }

        // Add profile image URL
        public string? ProfileImageUrl { get; set; }

        // Navigation properties for display
        public string CompanyName { get; set; }
        public string SubDepartmentName { get; set; }
        public string DepartmentName { get; set; }
        public string OccupationName { get; set; }
        public string? OccupationLevelName { get; set; }  // Add this
        public int? OccupationLevelRank { get; set; }     // Add this
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }

        // Application access information
        public List<EmployeeApplicationAccessDetailsDto> ApplicationAccesses { get; set; } = new List<EmployeeApplicationAccessDetailsDto>();
    }

    public class EmployeeApplicationAccessDetailsDto
    {
        public Guid ID { get; set; }
        public Guid ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string? ApplicationDescription { get; set; }
        public Guid AccessLevelID { get; set; }
        public string AccessLevelName { get; set; }
        public DateTime GrantedDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokedDate { get; set; }
        public string? GrantedByUserName { get; set; }
        public string? Remark { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class UpdateApplicationAccessDto
    {
        [Required]
        public Guid AccessLevelID { get; set; }
        
        [Required]
        public bool IsDeleted { get; set; }
        
        [Required]
        public Guid UpdatedBy { get; set; }
    }
    
    public class CreateApplicationAccessDto
    {
        public Guid ApplicationID { get; set; }
        public Guid AccessLevelID { get; set; }
        public DateTime GrantedDate { get; set; }
        public Guid GrantedBy { get; set; }
        public string? Remark { get; set; }
        public Guid CreatedBy { get; set; }
    }

    public class UploadProfileImageDto
    {
        [Required]
        public IFormFile ProfileImage { get; set; }
        
        [Required]
        public Guid UploadedBy { get; set; }
    }
}