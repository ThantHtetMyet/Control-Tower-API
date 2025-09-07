using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.RoomBookingSystem
{
    public class CreateRoomDto
    {
        [Required]
        public Guid BuildingID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string? Code { get; set; }

        // Floor property removed

        [Required]
        public int Capacity { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public string CreatedBy { get; set; }
    }

    public class UpdateRoomDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid BuildingID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string? Code { get; set; }

        // Floor property removed

        [Required]
        public int Capacity { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class RoomDto
    {
        public Guid ID { get; set; }
        public Guid BuildingID { get; set; }
        public string BuildingName { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        // Floor property removed
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
}