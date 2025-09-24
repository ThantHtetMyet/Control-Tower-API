using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ReportFormDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormTypeID { get; set; }
        public string ReportFormTypeName { get; set; }
        public string JobNo { get; set; }
        public Guid SystemNameWarehouseID { get; set; }
        public string SystemNameWarehouseName { get; set; }
        public Guid StationNameWarehouseID { get; set; }
        public string StationNameWarehouseName { get; set; }
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

        [Required(ErrorMessage = "JobNo is required")]
        [StringLength(255, ErrorMessage = "JobNo cannot exceed 255 characters")]
        public string JobNo { get; set; }

        [Required(ErrorMessage = "SystemNameWarehouseID is required")]
        public Guid SystemNameWarehouseID { get; set; }

        [Required(ErrorMessage = "StationNameWarehouseID is required")]
        public Guid StationNameWarehouseID { get; set; }

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
        

        [Required(ErrorMessage = "JobNo is required")]
        [StringLength(255, ErrorMessage = "JobNo cannot exceed 255 characters")]
        public string JobNo { get; set; }

        [Required(ErrorMessage = "SystemNameWarehouseID is required")]
        public Guid SystemNameWarehouseID { get; set; }

        [Required(ErrorMessage = "StationNameWarehouseID is required")]
        public Guid StationNameWarehouseID { get; set; }

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