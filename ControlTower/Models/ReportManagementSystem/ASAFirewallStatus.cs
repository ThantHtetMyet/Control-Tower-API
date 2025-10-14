using System.ComponentModel.DataAnnotations;

namespace ControlTower.Models.ReportManagementSystem
{
    public class ASAFirewallStatus
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(500)]
        public string CommandInput { get; set; }

        public int? SortOrder { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}