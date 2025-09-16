using System.ComponentModel.DataAnnotations;
using ControlTower.Models.ReportManagementSystem;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMChamberMagneticContactDto
    {
        public Guid ID { get; set; }
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? ChamberNumber { get; set; }
        public string? ChamberOGBox { get; set; }
        public string? ChamberContact1 { get; set; }
        public string? ChamberContact2 { get; set; }
        public string? ChamberContact3 { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? PMReportFormName { get; set; }
    }

    public class CreatePMChamberMagneticContactDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? ChamberNumber { get; set; }
        public string? ChamberOGBox { get; set; }
        public string? ChamberContact1 { get; set; }
        public string? ChamberContact2 { get; set; }
        public string? ChamberContact3 { get; set; }
    }

    public class UpdatePMChamberMagneticContactDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? ChamberNumber { get; set; }
        public string? ChamberOGBox { get; set; }
        public string? ChamberContact1 { get; set; }
        public string? ChamberContact2 { get; set; }
        public string? ChamberContact3 { get; set; }
    }
}