using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.EmployeeManagementSystem
{
    public class Company
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Remark { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        public User? UpdatedByUser { get; set; }

        // Navigation property for users belonging to this company
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}