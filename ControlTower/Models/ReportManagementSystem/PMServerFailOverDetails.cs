using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerFailOverDetails
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMServerFailOver")]
        public Guid PMServerFailOverID { get; set; }

        [ForeignKey("YesNoStatus")]
        public Guid YesNoStatusID { get; set; }

        public string? ToServer { get; set; }

        public string? FromServer { get; set; }

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
        public virtual PMServerFailOver PMServerFailOver { get; set; }
        public virtual YesNoStatus YesNoStatus { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}