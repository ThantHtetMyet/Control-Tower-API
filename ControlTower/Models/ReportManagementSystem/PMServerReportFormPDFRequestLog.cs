using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMServerReportFormPDFRequestLog
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("PMReportFormServer")]
        public Guid PMReportFormServerID { get; set; }

        [ForeignKey("RequestedByUser")]
        public Guid RequestedBy { get; set; }

        public DateTime RequestedDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual PMReportFormServer PMReportFormServer { get; set; }
        public virtual User RequestedByUser { get; set; }
    }
}