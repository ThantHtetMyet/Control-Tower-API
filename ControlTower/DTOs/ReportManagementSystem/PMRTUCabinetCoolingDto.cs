using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMRTUCabinetCoolingDto
    {
        public Guid ID { get; set; }
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? FanNumber { get; set; }
        public string? FunctionalStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? PMReportFormName { get; set; }
    }

    public class CreatePMRTUCabinetCoolingDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? FanNumber { get; set; }
        public string? FunctionalStatus { get; set; }
    }

    public class UpdatePMRTUCabinetCoolingDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? FanNumber { get; set; }
        public string? FunctionalStatus { get; set; }
    }
}