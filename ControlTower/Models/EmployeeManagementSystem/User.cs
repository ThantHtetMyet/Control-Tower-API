using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.EmployeeManagementSystem
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("Company")]
        public Guid CompanyID { get; set; }

        [ForeignKey("SubDepartment")]
        public Guid SubDepartmentID { get; set; }

        [ForeignKey("Occupation")]
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

        public int Rating { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime LastLogin { get; set; }

        public DateTime StartWorkingDate { get; set; }

        public DateTime? LastWorkingDate { get; set; }

        [StringLength(100)]
        public string? WorkPermit { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; }

        [StringLength(100)]
        public string? Religion { get; set; }

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

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual SubDepartment SubDepartment { get; set; }
        public virtual Occupation Occupation { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }

        // Self-referencing collections
        public virtual ICollection<User> CreatedUsers { get; set; } = new List<User>();
        public virtual ICollection<User> UpdatedUsers { get; set; } = new List<User>();
        public virtual ICollection<SubDepartment> CreatedSubDepartments { get; set; } = new List<SubDepartment>();
        public virtual ICollection<SubDepartment> UpdatedSubDepartments { get; set; } = new List<SubDepartment>();
        public virtual ICollection<Occupation> CreatedOccupations { get; set; } = new List<Occupation>();
        public virtual ICollection<Occupation> UpdatedOccupations { get; set; } = new List<Occupation>();
        
        // Add these missing Company navigation properties
        public virtual ICollection<Company> CreatedCompanies { get; set; } = new List<Company>();
        public virtual ICollection<Company> UpdatedCompanies { get; set; } = new List<Company>();
        
        // UserImage navigation property
        public virtual ICollection<UserImage> UserImages { get; set; } = new List<UserImage>();
    }
}