using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ReportFormImageType
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageTypeName { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}