using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerASAFirewall
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMReportFormServer")]
        public Guid PMReportFormServerID { get; set; }

        [ForeignKey("ASAFirewallStatus")]
        public Guid ASAFirewallStatusID { get; set; }

        [ForeignKey("ResultStatus")]
        public Guid ResultStatusID { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual PMReportFormServer PMReportFormServer { get; set; }
        public virtual ASAFirewallStatus ASAFirewallStatus { get; set; }
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}