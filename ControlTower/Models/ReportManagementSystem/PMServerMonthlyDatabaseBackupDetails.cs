using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerMonthlyDatabaseBackupDetails
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMServerMonthlyDatabaseBackup")]
        public Guid PMServerMonthlyDatabaseBackupID { get; set; }

        [StringLength(100)]
        public string? SerialNo { get; set; }

        [StringLength(500)]
        public string? ServerName { get; set; }

        [ForeignKey("YesNoStatus")]
        public Guid YesNoStatusID { get; set; }

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
        public virtual PMServerMonthlyDatabaseBackup PMServerMonthlyDatabaseBackup { get; set; }
        public virtual YesNoStatus YesNoStatus { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}