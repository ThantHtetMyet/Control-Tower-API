using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ASAFirewallStatusDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CommandInput { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CreateASAFirewallStatusDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Command Input cannot exceed 500 characters.")]
        public string CommandInput { get; set; }

        public int? SortOrder { get; set; }
    }

    public class UpdateASAFirewallStatusDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Command Input cannot exceed 500 characters.")]
        public string CommandInput { get; set; }

        public int? SortOrder { get; set; }
    }
}