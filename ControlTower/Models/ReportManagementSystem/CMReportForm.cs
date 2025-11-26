using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class CMReportForm
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("ServiceReportForm")]
        public Guid ReportFormID { get; set; }

        [ForeignKey("CMReportFormType")]
        public Guid CMReportFormTypeID { get; set; }
        public virtual CMReportFormType CMReportFormType { get; set; }

        [ForeignKey("FurtherActionTakenWarehouse")]
        public Guid? FurtherActionTakenID { get; set; }
        public virtual FurtherActionTakenWarehouse? FurtherActionTakenWarehouse { get; set; }

        [ForeignKey("FormStatusWarehouse")]
        public Guid FormstatusID { get; set; }
        public virtual FormStatusWarehouse FormStatusWarehouse { get; set; }
        
        [StringLength(500)]
        public string? Customer { get; set; }

        [StringLength(500)]
        public string? ReportTitle { get; set; }

        [StringLength(500)]
        public string? ProjectNo { get; set; }

        [StringLength(1000)]
        public string? IssueReportedDescription { get; set; }

        [StringLength(1000)]
        public string? IssueFoundDescription { get; set; }

        [StringLength(1000)]
        public string? ActionTakenDescription { get; set; }

        public DateTime? FailureDetectedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        [StringLength(500)]
        public string? AttendedBy { get; set; }

        [StringLength(500)]
        public string? ApprovedBy { get; set; }
        

        [StringLength(500)]
        public string? Remark { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        public virtual User? UpdatedByUser { get; set; }

        public virtual ReportForm ReportForm { get; set; }
    }
}