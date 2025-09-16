using ControlTower.Models.EmployeeManagementSystem;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.ReportManagementSystem
{
    public class StationNameWarehouse
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid SystemNameWarehouseID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        // Navigation Properties
        [ForeignKey("SystemNameWarehouseID")]
        public virtual SystemNameWarehouse SystemNameWarehouse { get; set; }

        // Reverse navigation
        public virtual ICollection<ReportForm> ReportForms { get; set; } = new List<ReportForm>();
    }
}