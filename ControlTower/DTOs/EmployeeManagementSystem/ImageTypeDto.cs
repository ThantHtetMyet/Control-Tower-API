using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.EmployeeManagementSystem
{
    public class ImageTypeDto
    {
        public Guid ID { get; set; }
        public string TypeName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedByName { get; set; }
    }

    public class CreateImageTypeDto
    {
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }

    public class UpdateImageTypeDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        public Guid UpdatedBy { get; set; }
    }
}