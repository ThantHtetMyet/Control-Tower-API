using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class CMMaterialUsed
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("CMReportForm")]
        public Guid CMReportFormID { get; set; }
        public virtual CMReportForm CMReportForm { get; set; }

        public string? ItemDescription { get; set; }

        public string? NewSerialNo { get; set; }

        public string? OldSerialNo { get; set; }

        public string? Remark { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}