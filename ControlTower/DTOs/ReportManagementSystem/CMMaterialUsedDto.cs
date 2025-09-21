using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class CMMaterialUsedDto
    {
        public Guid ID { get; set; }
        public Guid CMReportFormID { get; set; }
        public string? CMReportFormName { get; set; }
        public string? ItemDescription { get; set; }
        public string? NewSerialNo { get; set; }
        public string? OldSerialNo { get; set; }
        public string? Remark { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
    }

    public class CreateCMMaterialUsedDto
    {
        [Required(ErrorMessage = "CM Report Form ID is required")]
        public Guid CMReportFormID { get; set; }

        [StringLength(1000, ErrorMessage = "Item Description cannot exceed 1000 characters")]
        public string? ItemDescription { get; set; }

        [StringLength(500, ErrorMessage = "New Serial Number cannot exceed 500 characters")]
        public string? NewSerialNo { get; set; }

        [StringLength(500, ErrorMessage = "Old Serial Number cannot exceed 500 characters")]
        public string? OldSerialNo { get; set; }

        [StringLength(1000, ErrorMessage = "Remark cannot exceed 1000 characters")]
        public string? Remark { get; set; }

        [Required(ErrorMessage = "Created By is required")]
        public Guid CreatedBy { get; set; }
    }

    public class UpdateCMMaterialUsedDto
    {
        [StringLength(1000, ErrorMessage = "Item Description cannot exceed 1000 characters")]
        public string? ItemDescription { get; set; }

        [StringLength(500, ErrorMessage = "New Serial Number cannot exceed 500 characters")]
        public string? NewSerialNo { get; set; }

        [StringLength(500, ErrorMessage = "Old Serial Number cannot exceed 500 characters")]
        public string? OldSerialNo { get; set; }

        [StringLength(1000, ErrorMessage = "Remark cannot exceed 1000 characters")]
        public string? Remark { get; set; }

        [Required(ErrorMessage = "Updated By is required")]
        public Guid UpdatedBy { get; set; }
    }
}