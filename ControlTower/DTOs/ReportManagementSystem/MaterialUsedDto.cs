using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class MaterialUsedDto
    {
        public Guid ID { get; set; }
        public Guid CMReportFormID { get; set; }
        public string? CMReportFormName { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string? SerialNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
    }

    public class CreateMaterialUsedDto
    {
        [Required(ErrorMessage = "CM Report Form ID is required")]
        public Guid CMReportFormID { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "Serial Number cannot exceed 500 characters")]
        public string? SerialNo { get; set; }

        [Required(ErrorMessage = "Created By is required")]
        public Guid CreatedBy { get; set; }
    }

    public class UpdateMaterialUsedDto
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "Serial Number cannot exceed 500 characters")]
        public string? SerialNo { get; set; }

        [Required(ErrorMessage = "Updated By is required")]
        public Guid UpdatedBy { get; set; }
    }
}