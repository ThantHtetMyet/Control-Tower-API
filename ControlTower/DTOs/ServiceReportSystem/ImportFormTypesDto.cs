using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class ImportFormTypesDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public List<ImportFileRecordsDto>? ImportFileRecords { get; set; }
    }

    public class CreateImportFormTypesDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateImportFormTypesDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ImportFileRecordsDto
    {
        public Guid ID { get; set; }
        public Guid ImportFormTypeID { get; set; }
        public string ImportFormTypeName { get; set; }
        public string Name { get; set; }
        public string? StoredDirectory { get; set; }
        public string? ImportedStatus { get; set; }
        public string? UploadedStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? UploadedDate { get; set; }
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
        [StringLength(500)]
        public string? StoredDirectory { get; set; }
        [StringLength(50)]
        public string? ImportedStatus { get; set; }
        [StringLength(50)]
        public string? UploadedStatus { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string? UploadedBy { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateImportFileRecordsDto
    {
        public Guid ImportFormTypeID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(500)]
        public string? StoredDirectory { get; set; }
        [StringLength(50)]
        public string? ImportedStatus { get; set; }
        [StringLength(50)]
        public string? UploadedStatus { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string? UploadedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}