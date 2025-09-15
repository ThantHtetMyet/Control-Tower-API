using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ReportFormDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormTypeID { get; set; }
        public string ReportFormTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? UploadStatus { get; set; }
        public string? UploadHostname { get; set; }
        public string? UploadIPAddress { get; set; }
        public string? FormStatus { get; set; }
    }

    public class CreateReportFormDto
    {
        [Required(ErrorMessage = "ReportFormTypeID is required")]
        public Guid ReportFormTypeID { get; set; }

        [StringLength(255, ErrorMessage = "UploadStatus cannot exceed 255 characters")]
        public string? UploadStatus { get; set; }

        [StringLength(255, ErrorMessage = "UploadHostname cannot exceed 255 characters")]
        public string? UploadHostname { get; set; }

        [StringLength(45, ErrorMessage = "UploadIPAddress cannot exceed 45 characters")]
        public string? UploadIPAddress { get; set; }

        [StringLength(255, ErrorMessage = "FormStatus cannot exceed 255 characters")]
        public string? FormStatus { get; set; }
    }

    public class UpdateReportFormDto
    {
        [Required(ErrorMessage = "ReportFormTypeID is required")]
        public Guid ReportFormTypeID { get; set; }

        [StringLength(255, ErrorMessage = "UploadStatus cannot exceed 255 characters")]
        public string? UploadStatus { get; set; }

        [StringLength(255, ErrorMessage = "UploadHostname cannot exceed 255 characters")]
        public string? UploadHostname { get; set; }

        [StringLength(45, ErrorMessage = "UploadIPAddress cannot exceed 45 characters")]
        public string? UploadIPAddress { get; set; }

        [StringLength(255, ErrorMessage = "FormStatus cannot exceed 255 characters")]
        public string? FormStatus { get; set; }
    }
}