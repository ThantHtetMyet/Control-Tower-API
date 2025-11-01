using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMServerReportFormPDFRequestLogDto
    {
        public Guid ID { get; set; }
        public Guid PMReportFormServerID { get; set; }
        public Guid RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties for display
        public string? RequestedByUserName { get; set; }
        public string? PMReportFormServerTitle { get; set; }
    }

    public class CreatePMServerReportFormPDFRequestLogDto
    {
        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        public Guid RequestedBy { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; }
    }

    public class UpdatePMServerReportFormPDFRequestLogDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid PMReportFormServerID { get; set; }

        [Required]
        public Guid RequestedBy { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; }
    }
}