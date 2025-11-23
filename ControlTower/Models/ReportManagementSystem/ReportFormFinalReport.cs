using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ReportFormFinalReport
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("ReportForm")]
        public Guid ReportFormID { get; set; }

        [Required]
        [StringLength(255)]
        public string AttachmentName { get; set; }

        [Required]
        [StringLength(255)]
        public string AttachmentPath { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("UploadedByUser")]
        public Guid UploadedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        public virtual ReportForm ReportForm { get; set; }
        public virtual User UploadedByUser { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}
