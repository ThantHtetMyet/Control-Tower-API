using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMMainRtuCabinet
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMReportFormRTUID")]
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

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual PMReportFormRTU? PMReportForm { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}