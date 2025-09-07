using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.RoomBookingSystem
{
    public class CreateBuildingDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string? Code { get; set; }

        [StringLength(300)]
        public string? Address { get; set; }

        public string CreatedBy { get; set; }
    }

    public class UpdateBuildingDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string? Code { get; set; }

        [StringLength(300)]
        public string? Address { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class BuildingDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
}