using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.EmployeeManagementSystem
{
    public class AccessLevel
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string LevelName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

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
        public virtual ICollection<UserApplicationAccess> UserApplicationAccesses { get; set; } = new List<UserApplicationAccess>();
    }
}