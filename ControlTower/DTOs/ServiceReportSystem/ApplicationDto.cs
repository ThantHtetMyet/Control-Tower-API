using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class CreateApplicationDto
    {
        [Required]
        [StringLength(100)]
        public string ApplicationName { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        public Guid CreatedBy { get; set; }
    }

    public class UpdateApplicationDto
    {
        [Required]
        public Guid ID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ApplicationName { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        
        public Guid? UpdatedBy { get; set; }
    }

    public class ApplicationDto
    {
        public Guid ID { get; set; }
        public string ApplicationName { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
}