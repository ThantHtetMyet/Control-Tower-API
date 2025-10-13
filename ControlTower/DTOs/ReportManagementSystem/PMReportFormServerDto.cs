using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMReportFormServerDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormID { get; set; }
        public Guid PMReportFormTypeID { get; set; }
        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public string? ReportTitle { get; set; }
        public string? AttendedBy { get; set; }
        public string? WitnessedBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Remarks { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties for display
        public string? PMReportFormTypeName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? JobNo { get; set; } // From parent ReportForm
        public string? StationName { get; set; }
        public string? SystemDescription { get; set; }
    }

    public class CreatePMReportFormServerDto
    {
        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        public Guid PMReportFormTypeID { get; set; }

        [StringLength(255)]
        public string? ProjectNo { get; set; }

        [StringLength(255)]
        public string? Customer { get; set; }

        [StringLength(255)]
        public string? ReportTitle { get; set; }

        [StringLength(255)]
        public string? AttendedBy { get; set; }

        [StringLength(255)]
        public string? WitnessedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }

    public class UpdatePMReportFormServerDto
    {
        [Required]
        public Guid PMReportFormTypeID { get; set; }

        [StringLength(255)]
        public string? ProjectNo { get; set; }

        [StringLength(255)]
        public string? Customer { get; set; }

        [StringLength(255)]
        public string? ReportTitle { get; set; }

        [StringLength(255)]
        public string? AttendedBy { get; set; }

        [StringLength(255)]
        public string? WitnessedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}