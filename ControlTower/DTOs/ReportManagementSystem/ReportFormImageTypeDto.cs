using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ReportFormImageTypeDto
    {
        public Guid ID { get; set; }
        public string ImageTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties for display
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreateReportFormImageTypeDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Image type name cannot exceed 255 characters.")]
        public string ImageTypeName { get; set; }
    }

    public class UpdateReportFormImageTypeDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Image type name cannot exceed 255 characters.")]
        public string ImageTypeName { get; set; }
    }
}