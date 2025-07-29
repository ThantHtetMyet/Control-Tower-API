using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class OccupationDto
    {
        public Guid ID { get; set; }
        public string OccupationName { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
    
    public class CreateOccupationDto
    {
        [Required]
        [StringLength(100)]
        public string OccupationName { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        public int Rating { get; set; }
    }
    
    public class UpdateOccupationDto
    {
        [Required]
        [StringLength(100)]
        public string OccupationName { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(200)]
        public string? Remark { get; set; }
        
        public int Rating { get; set; }
    }
}