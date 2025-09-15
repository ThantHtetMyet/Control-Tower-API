using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ReportFormTypeDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties for display
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreateReportFormTypeDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; }
    }

    public class UpdateReportFormTypeDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; }
    }
}