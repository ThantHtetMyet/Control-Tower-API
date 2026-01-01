using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ServerHostNameWarehouse
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("StationNameWarehouse")]
        public Guid StationNameID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation Properties
        [ForeignKey("StationNameID")]
        public virtual StationNameWarehouse? StationNameWarehouse { get; set; }
    }
}

