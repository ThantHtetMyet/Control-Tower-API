using System.ComponentModel.DataAnnotations;

namespace ControlTower.Models.RoomBookingSystem
{
    public class RoomBookingStatus
    {
        [Key]
        public Guid ID { get; set; } // Changed from int to Guid

        [Required]
        [StringLength(20)]
        public string Name { get; set; } // Unique name for the status

        [Required]
        public bool IsDeleted { get; set; }
    }
}