using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class CreateAccessLevelDto
    {
        [Required]
        [StringLength(100)]
        public string LevelName { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public Guid CreatedBy { get; set; }
    }

    public class UpdateAccessLevelDto
    {
        [Required]
        public Guid ID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LevelName { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public Guid? UpdatedBy { get; set; }
    }

    public class AccessLevelDto
    {
        public Guid ID { get; set; }
        public string LevelName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
}