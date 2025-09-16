using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMDVREquipmentDto
    {
        public Guid ID { get; set; }
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? DVRComm { get; set; }
        public string? DVRRAIDComm { get; set; }
        public string? TimeSyncNTPServer { get; set; }
        public string? Recording24x7 { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? PMReportFormName { get; set; }
    }

    public class CreatePMDVREquipmentDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? DVRComm { get; set; }
        public string? DVRRAIDComm { get; set; }
        public string? TimeSyncNTPServer { get; set; }
        public string? Recording24x7 { get; set; }
    }

    public class UpdatePMDVREquipmentDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? Remarks { get; set; }
        public string? DVRComm { get; set; }
        public string? DVRRAIDComm { get; set; }
        public string? TimeSyncNTPServer { get; set; }
        public string? Recording24x7 { get; set; }
    }
}