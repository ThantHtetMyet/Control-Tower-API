using ControlTower.Models.EmployeeManagementSystem;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.ReportManagementSystem
{
    public class SystemNameWarehouse
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        // Reverse navigation
        public virtual ICollection<StationNameWarehouse> StationNameWarehouses { get; set; } = new List<StationNameWarehouse>();
        public virtual ICollection<ReportForm> ReportForms { get; set; } = new List<ReportForm>();
    }
}