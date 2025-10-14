using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ServerDiskStatus
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}