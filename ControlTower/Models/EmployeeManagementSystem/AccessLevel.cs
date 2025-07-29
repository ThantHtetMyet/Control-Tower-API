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

        [ForeignKey("CreatedByEmployee")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByEmployee")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual User? CreatedByEmployee { get; set; }
        public virtual User? UpdatedByEmployee { get; set; }
        public virtual ICollection<UserApplicationAccess> UserApplicationAccesses { get; set; } = new List<UserApplicationAccess>();
    }
}