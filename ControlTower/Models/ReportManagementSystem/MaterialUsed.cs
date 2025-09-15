using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
namespace ControlTower.Models.ReportManagementSystem
{
    public class MaterialUsed
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("CMReportForm")]
        public Guid CMReportFormID { get; set; }
        public virtual CMReportForm CMReportForm { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? SerialNo { get; set; }

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