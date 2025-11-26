using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class CMReportFormDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormID { get; set; }
        public Guid CMReportFormTypeID { get; set; }
        public Guid? FurtherActionTakenID { get; set; }
        public Guid FormstatusID { get; set; }
        public string? Customer { get; set; }
        public string? ReportTitle { get; set; }
        public string? ProjectNo { get; set; }
        public string? IssueReportedDescription { get; set; }
        public string? IssueFoundDescription { get; set; }
        public string? ActionTakenDescription { get; set; }
        public DateTime? FailureDetectedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Remark { get; set; }

        // Navigation properties for display
        public string? ReportFormTypeName { get; set; }
        public string? FurtherActionTakenName { get; set; }
        public string? FormStatusName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? JobNo { get; set; } // Add JobNo from parent ReportForm

        public string? stationName { get; set; }

        public string? systemDescription { get; set; }
    }

    public class CreateCMReportFormDto
    {
        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        public Guid CMReportFormTypeID { get; set; }

        
        public Guid? FurtherActionTakenID { get; set; }

        [Required]
        public Guid FormstatusID { get; set; }

        public string? Customer { get; set; }

        public string? ReportTitle { get; set; }

        public string? ProjectNo { get; set; }

        // Removed: SystemDescription property
        public string? IssueReportedDescription { get; set; }
        public string? IssueFoundDescription { get; set; }
        public string? ActionTakenDescription { get; set; }
        public DateTime? FailureDetectedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? Remark { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }

    public class UpdateCMReportFormDto
    {
        [Required]
        public Guid CMReportFormTypeID { get; set; }

        
        public Guid? FurtherActionTakenID { get; set; }

        [Required]
        public Guid FormstatusID { get; set; }

        public string? Customer { get; set; }

        public string? ReportTitle { get; set; }

        public string? ProjectNo { get; set; }

        // Removed: SystemDescription property
        public string? IssueReportedDescription { get; set; }

        public string? IssueFoundDescription { get; set; }

        public string? ActionTakenDescription { get; set; }


        public DateTime? FailureDetectedDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public string? AttendedBy { get; set; }

        public string? ApprovedBy { get; set; }
        public string? Remark { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}