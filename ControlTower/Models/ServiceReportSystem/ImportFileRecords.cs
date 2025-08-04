using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.ServiceReportSystem
{
    public class ImportFileRecords
    {
        [Key]
        public Guid ID { get; set; }
        
        [ForeignKey("ImportFormType")]
        public Guid ImportFormTypeID { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ImportFormTypes ImportFormType { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        
        [Column(TypeName = "nvarchar(500)")]
        public string? StoredDirectory { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string? ImportedStatus { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string? UploadedStatus { get; set; }
        
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        [Column(TypeName = "Date")]
        public DateTime? UploadedDate { get; set; }
        
        [ForeignKey("UploadedByUser")]
        public Guid? UploadedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? UploadedByUser { get; set; }
        
        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? CreatedByUser { get; set; }
        
        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? UpdatedByUser { get; set; }
    }
}