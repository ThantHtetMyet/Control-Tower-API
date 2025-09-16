using System.ComponentModel.DataAnnotations;
using ControlTower.Models.ReportManagementSystem;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMMainRtuCabinetDto
    {
        public Guid ID { get; set; }
        public Guid PMReportFormRTUID { get; set; }
        public string? RTUCabinet { get; set; }
        public string? EquipmentRack { get; set; }
        public string? Monitor { get; set; }
        public string? MouseKeyboard { get; set; }
        public string? CPU6000Card { get; set; }
        public string? InputCard { get; set; }
        public string? MegapopNTU { get; set; }
        public string? NetworkRouter { get; set; }
        public string? NetworkSwitch { get; set; }
        public string? DigitalVideoRecorder { get; set; }
        public string? RTUDoorContact { get; set; }
        public string? PowerSupplyUnit { get; set; }
        public string? UPSTakingOverTest { get; set; }
        public string? UPSBattery { get; set; }
        public string? Remarks { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? PMReportFormName { get; set; }
    }

    public class CreatePMMainRtuCabinetDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? RTUCabinet { get; set; }
        public string? EquipmentRack { get; set; }
        public string? Monitor { get; set; }
        public string? MouseKeyboard { get; set; }
        public string? CPU6000Card { get; set; }
        public string? InputCard { get; set; }
        public string? MegapopNTU { get; set; }
        public string? NetworkRouter { get; set; }
        public string? NetworkSwitch { get; set; }
        public string? DigitalVideoRecorder { get; set; }
        public string? RTUDoorContact { get; set; }
        public string? PowerSupplyUnit { get; set; }
        public string? UPSTakingOverTest { get; set; }
        public string? UPSBattery { get; set; }
        public string? Remarks { get; set; }
    }

    public class UpdatePMMainRtuCabinetDto
    {
        [Required]
        public Guid PMReportFormRTUID { get; set; }
        public string? RTUCabinet { get; set; }
        public string? EquipmentRack { get; set; }
        public string? Monitor { get; set; }
        public string? MouseKeyboard { get; set; }
        public string? CPU6000Card { get; set; }
        public string? InputCard { get; set; }
        public string? MegapopNTU { get; set; }
        public string? NetworkRouter { get; set; }
        public string? NetworkSwitch { get; set; }
        public string? DigitalVideoRecorder { get; set; }
        public string? RTUDoorContact { get; set; }
        public string? PowerSupplyUnit { get; set; }
        public string? UPSTakingOverTest { get; set; }
        public string? UPSBattery { get; set; }
        public string? Remarks { get; set; }
    }
}