using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ReportFormFinalReportDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormID { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UploadedDate { get; set; }
        public Guid UploadedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public string? ReportFormTypeName { get; set; }
        public string? UploadedByUserName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreateReportFormFinalReportDto
    {
        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        [StringLength(255)]
        public string AttachmentName { get; set; }

        [Required]
        [StringLength(255)]
        public string AttachmentPath { get; set; }

        [Required]
        public Guid UploadedBy { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }

    public class UpdateReportFormFinalReportDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string AttachmentName { get; set; }

        [Required]
        [StringLength(255)]
        public string AttachmentPath { get; set; }

        [Required]
        public Guid UploadedBy { get; set; }

        [Required]
        public Guid UpdatedBy { get; set; }

        public DateTime? UploadedDate { get; set; }
    }
}
