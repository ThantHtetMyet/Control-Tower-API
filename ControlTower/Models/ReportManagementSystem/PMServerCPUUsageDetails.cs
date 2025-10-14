using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerCPUUsageDetails
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMServerCPUAndMemoryUsage")]
        public Guid PMServerCPUAndMemoryUsageID { get; set; }

        [StringLength(100)]
        public string? SerialNo { get; set; }

        [StringLength(500)]
        public string? ServerName { get; set; }

        [StringLength(100)]
        public string? CPUUsage { get; set; }

        [ForeignKey("ResultStatus")]
        public Guid ResultStatusID { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual PMServerCPUAndMemoryUsage PMServerCPUAndMemoryUsage { get; set; }
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}