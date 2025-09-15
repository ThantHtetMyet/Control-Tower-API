using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ImportFileRecordsDto
    {
        public Guid ID { get; set; }
        public Guid ImportFormTypeID { get; set; }
        public string Name { get; set; }
        public string StoredDirectory { get; set; }
        public string ImportedStatus { get; set; }
        public string UploadedStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UploadedDate { get; set; }
        public Guid? UploadedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties for display
        public string? ImportFormTypeName { get; set; }
        public string? UploadedByUserName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreateImportFileRecordsDto
    {
        [Required]
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

        public DateTime? UploadedDate { get; set; }
        public Guid? UploadedBy { get; set; }
    }

    public class UpdateImportFileRecordsDto
    {
        [Required]
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

        public DateTime? UploadedDate { get; set; }
        public Guid? UploadedBy { get; set; }
    }
}