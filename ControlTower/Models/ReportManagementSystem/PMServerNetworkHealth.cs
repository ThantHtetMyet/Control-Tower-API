using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerNetworkHealth
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid PMReportFormServerID { get; set; }

        [Required]
        public Guid YesNoStatusID { get; set; }

        public DateTime? DateChecked { get; set; }

        public string? Remarks { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public Guid UpdatedBy { get; set; }

        // Navigation properties
        [ForeignKey("PMReportFormServerID")]
        public virtual PMReportFormServer PMReportFormServer { get; set; }

        [ForeignKey("YesNoStatusID")]
        public virtual YesNoStatus YesNoStatus { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }
    }
}