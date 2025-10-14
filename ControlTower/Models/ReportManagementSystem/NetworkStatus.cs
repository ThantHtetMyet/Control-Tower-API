using System.ComponentModel.DataAnnotations;

namespace ControlTower.Models.ReportManagementSystem
{
    public class NetworkStatus
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}