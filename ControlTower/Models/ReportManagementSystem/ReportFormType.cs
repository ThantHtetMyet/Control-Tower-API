using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ReportFormType
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}