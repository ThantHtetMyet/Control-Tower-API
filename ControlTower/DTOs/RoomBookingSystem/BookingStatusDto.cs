using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.RoomBookingSystem
{
    public class CreateBookingStatusDto
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        
        // No ID field as it will be generated on the server
    }

    public class UpdateBookingStatusDto
    {
        [Required]
        public Guid ID { get; set; } // Changed from int to Guid

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }

    public class BookingStatusDto
    {
        public Guid ID { get; set; } // Changed from int to Guid
        public string Name { get; set; }
    }
}