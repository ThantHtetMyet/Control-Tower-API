using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerMonthlyDatabaseBackup
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMReportFormServer")]
        public Guid PMReportFormServerID { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        [StringLength(500)]
        public string? LatestBackupFileName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual PMReportFormServer PMReportFormServer { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
        public virtual ICollection<PMServerMonthlyDatabaseBackupDetails> PMServerMonthlyDatabaseBackupDetails { get; set; } = new List<PMServerMonthlyDatabaseBackupDetails>();
        public virtual ICollection<PMServerMonthlySCADADataBackupDetails> PMServerMonthlySCADADataBackupDetails { get; set; } = new List<PMServerMonthlySCADADataBackupDetails>();
    }
}