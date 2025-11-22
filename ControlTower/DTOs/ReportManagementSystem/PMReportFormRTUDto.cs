using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMReportFormRTUDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormID { get; set; }
        public Guid PMReportFormTypeID { get; set; }
        public Guid FormstatusID { get; set; }
        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public DateTime? DateOfService { get; set; }
        public string? CleaningOfCabinet { get; set; }
        public string? Remarks { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties for display
        public string? PMReportFormTypeName { get; set; }
        public string? FormStatusName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreatePMReportFormRTUDto
    {
        [Required]
        public Guid ReportFormID { get; set; }
        
        [Required]
        public Guid PMReportFormTypeID { get; set; }
        [Required]
        public Guid FormstatusID { get; set; }
        

        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public DateTime? DateOfService { get; set; }
        public string? CleaningOfCabinet { get; set; }
        public string? Remarks { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }

        [StringLength(500)]
        public string? ReportTitle { get; set; }
    }

    public class UpdatePMReportFormRTUDto
    {
        public Guid? FormstatusID { get; set; }
        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public DateTime? DateOfService { get; set; }
        public string? CleaningOfCabinet { get; set; }
        public string? Remarks { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }
    }
}
