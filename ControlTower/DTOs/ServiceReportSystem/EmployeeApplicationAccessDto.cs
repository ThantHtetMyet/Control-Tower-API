using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class CreateEmployeeApplicationAccessDto
    {
        [Required]
        public Guid EmployeeID { get; set; }
        
        [Required]
        public Guid ApplicationID { get; set; }
        
        [Required]
        public Guid AccessLevelID { get; set; }  // Removed [StringLength(50)]
        
        [Required]
        public DateTime GrantedDate { get; set; }
        
        public Guid? GrantedBy { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        public Guid CreatedBy { get; set; }
    }

    public class UpdateEmployeeApplicationAccessDto
    {
        [Required]
        public Guid ID { get; set; }
        
        [Required]
        public Guid EmployeeID { get; set; }
        
        [Required]
        public Guid ApplicationID { get; set; }
        
        [Required]
        public Guid AccessLevelID { get; set; }  // Removed [StringLength(50)]
        
        [Required]
        public DateTime GrantedDate { get; set; }
        
        public bool IsRevoked { get; set; }
        
        public DateTime? RevokedDate { get; set; }
        
        public Guid? GrantedBy { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        public Guid? UpdatedBy { get; set; }
    }

    public class EmployeeApplicationAccessDto
    {
        public Guid ID { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid ApplicationID { get; set; }
        public Guid AccessLevelID { get; set; }
        public DateTime GrantedDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokedDate { get; set; }
        public Guid? GrantedBy { get; set; }
        public string? Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        // Navigation properties for display
        public string EmployeeName { get; set; }
        public string EmployeeStaffCardID { get; set; }
        public string ApplicationName { get; set; }
        public string? GrantedByUserName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
}