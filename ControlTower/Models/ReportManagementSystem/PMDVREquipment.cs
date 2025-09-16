using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMDVREquipment
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMReportFormRTUID")]
        public Guid PMReportFormRTUID { get; set; }

        public string? Remarks { get; set; }

        public string? DVRComm { get; set; }
        public string? DVRRAIDComm { get; set; }
        public string? TimeSyncNTPServer { get; set; }
        public string? Recording24x7 { get; set; }

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