using System.ComponentModel.DataAnnotations;

namespace ControlTower.Models.ReportManagementSystem
{
    public class WillowlynxNetworkStatus
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}