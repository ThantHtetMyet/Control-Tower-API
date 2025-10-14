using System.ComponentModel.DataAnnotations;

namespace ControlTower.Models.ReportManagementSystem
{
    public class FailOverStatus
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int? SortOrder { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}