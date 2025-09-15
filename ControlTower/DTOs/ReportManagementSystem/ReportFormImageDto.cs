using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ReportFormImageDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormID { get; set; }
        public Guid ReportImageTypeID { get; set; }
        public string ImageName { get; set; }
        public string StoredDirectory { get; set; }
        public string UploadedStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UploadedDate { get; set; }
        public Guid UploadedBy { get; set; }

        // Navigation properties for display
        public string? ReportFormTypeName { get; set; }
        public string? ImageTypeName { get; set; }
        public string? UploadedByUserName { get; set; }
    }

    public class CreateReportFormImageDto
    {
        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        public Guid ReportImageTypeID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Image name cannot exceed 255 characters.")]
        public string ImageName { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Stored directory cannot exceed 500 characters.")]
        public string StoredDirectory { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Uploaded status cannot exceed 50 characters.")]
        public string UploadedStatus { get; set; }

        [Required]
        public Guid UploadedBy { get; set; }
    }

    public class UpdateReportFormImageDto
    {
        [Required]
        public Guid ReportImageTypeID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Image name cannot exceed 255 characters.")]
        public string ImageName { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Stored directory cannot exceed 500 characters.")]
        public string StoredDirectory { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Uploaded status cannot exceed 50 characters.")]
        public string UploadedStatus { get; set; }
    }
}