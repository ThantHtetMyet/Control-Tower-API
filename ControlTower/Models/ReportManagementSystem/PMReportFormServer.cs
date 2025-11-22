using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMReportFormServer
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("ReportForm")]
        public Guid ReportFormID { get; set; }

        [ForeignKey("PMReportFormType")]
        public Guid PMReportFormTypeID { get; set; }

        [StringLength(500)]
        public string? ProjectNo { get; set; }

        [StringLength(500)]
        public string? Customer { get; set; }

        [StringLength(500)]
        public string? ReportTitle { get; set; }

        [StringLength(500)]
        public string? AttendedBy { get; set; }

        [StringLength(500)]
        public string? WitnessedBy { get; set; }
        [ForeignKey("FormStatusWarehouse")]
        public Guid FormstatusID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual ReportForm ReportForm { get; set; }
        public virtual PMReportFormType PMReportFormType { get; set; }
        public virtual FormStatusWarehouse? FormStatusWarehouse { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}
