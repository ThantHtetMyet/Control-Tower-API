using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerDiskUsageHealthDetails
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMServerDiskUsageHealth")]
        public Guid PMServerDiskUsageHealthID { get; set; }

        [ForeignKey("ServerDiskStatus")]
        public Guid ServerDiskStatusID { get; set; }

        [ForeignKey("ResultStatus")]
        public Guid ResultStatusID { get; set; }

        [StringLength(500)]
        public string? DiskName { get; set; }

        [StringLength(500)]
        public string? ServerName { get; set; }

        [StringLength(100)]
        public string? Capacity { get; set; }

        [StringLength(100)]
        public string? FreeSpace { get; set; }

        [StringLength(100)]
        public string? Usage { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        /// <summary>
        /// Server Entry Index: Used to distinguish between duplicate server names.
        /// Disks with the same ServerName and ServerEntryIndex belong to the same server entry.
        /// </summary>
        public int? ServerEntryIndex { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual PMServerDiskUsageHealth PMServerDiskUsageHealth { get; set; }
        public virtual ServerDiskStatus ServerDiskStatus { get; set; }
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}