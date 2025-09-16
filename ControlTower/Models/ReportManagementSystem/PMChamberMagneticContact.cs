using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMChamberMagneticContact
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMReportFormRTUID")]
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