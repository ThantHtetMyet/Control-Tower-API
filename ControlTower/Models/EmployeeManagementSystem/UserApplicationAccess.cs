using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.EmployeeManagementSystem
{
    public class UserApplicationAccess
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("Employee")]
        public Guid EmployeeID { get; set; }

        [ForeignKey("Application")]
        public Guid ApplicationID { get; set; }

        [ForeignKey("AccessLevel")]
        public Guid AccessLevelID { get; set; }

        public DateTime GrantedDate { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? RevokedDate { get; set; }

        [ForeignKey("GrantedByEmployee")]
        public Guid? GrantedBy { get; set; }

        [StringLength(200)]
        public string? Remark { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByEmployee")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByEmployee")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual User Employee { get; set; }
        public virtual Application Application { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual User? GrantedByEmployee { get; set; }
        public virtual User? CreatedByEmployee { get; set; }
        public virtual User? UpdatedByEmployee { get; set; }
    }
}