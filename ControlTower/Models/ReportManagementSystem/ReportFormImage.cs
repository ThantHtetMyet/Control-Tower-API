using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ReportFormImage
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        public Guid ReportImageTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageName { get; set; }

        [Required]
        [StringLength(500)]
        public string StoredDirectory { get; set; }

        [Required]
        [StringLength(50)]
        public string UploadedStatus { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid UploadedBy { get; set; }

        // Navigation Properties
        [ForeignKey("ReportFormID")]
        public virtual ReportForm ServiceReportForm { get; set; }

        [ForeignKey("ReportImageTypeID")]
        public virtual ReportFormImageType ReportFormImageType { get; set; }

        [ForeignKey("UploadedBy")]
        public virtual User UploadedByUser { get; set; }
    }
}