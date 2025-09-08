using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.EmployeeManagementSystem
{
    public class CreateOccupationLevelDto
    {
        [Required]
        [StringLength(100)]
        public string LevelName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int? Rank { get; set; }

        public string CreatedBy { get; set; }
    }

    public class UpdateOccupationLevelDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string LevelName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int? Rank { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class OccupationLevelDto
    {
        public Guid ID { get; set; }
        public string LevelName { get; set; }
        public string? Description { get; set; }
        public int? Rank { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public int EmployeeCount { get; set; }
    }
}