using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ImportFileRecords
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("ImportFormType")]
        public Guid ImportFormTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string StoredDirectory { get; set; }

        [Required]
        [StringLength(50)]
        public string ImportedStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string UploadedStatus { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? UploadedDate { get; set; }

        [ForeignKey("UploadedByUser")]
        public Guid? UploadedBy { get; set; }

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual ImportFormTypes ImportFormType { get; set; }
        public virtual User? UploadedByUser { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}