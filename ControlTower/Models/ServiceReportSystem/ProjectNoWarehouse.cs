
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.ServiceReportSystem
{
    public class ProjectNoWarehouse
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ProjectNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
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