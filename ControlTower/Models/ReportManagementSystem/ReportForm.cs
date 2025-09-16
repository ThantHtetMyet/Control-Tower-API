using ControlTower.Models.EmployeeManagementSystem;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ReportForm
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid ReportFormTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string JobNo { get; set; }

        [Required]
        public Guid SystemNameWarehouseID { get; set; }

        [Required]
        public Guid StationNameWarehouseID { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        [StringLength(255)]
        public string? UploadStatus { get; set; }

        [StringLength(255)]
        public string? UploadHostname { get; set; }

        [StringLength(45)]
        public string? UploadIPAddress { get; set; }

        [StringLength(255)]
        public string? FormStatus { get; set; }

        // Navigation Properties
        [ForeignKey("ReportFormTypeID")]
        public virtual ReportFormType ReportFormType { get; set; }

        [ForeignKey("SystemNameWarehouseID")]
        public virtual SystemNameWarehouse SystemNameWarehouse { get; set; }

        [ForeignKey("StationNameWarehouseID")]
        public virtual StationNameWarehouse StationNameWarehouse { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual User? UpdatedByUser { get; set; }
    }
}