using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class CreateDepartmentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        public string CreatedBy { get; set; }
    }

    public class UpdateDepartmentDto
    {
        [Required]
        public Guid ID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        public string? UpdatedBy { get; set; }
    }

    public class DepartmentDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public int EmployeeCount { get; set; }
    }
}