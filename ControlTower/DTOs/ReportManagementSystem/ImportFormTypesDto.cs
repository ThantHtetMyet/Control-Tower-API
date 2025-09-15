using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ImportFormTypesDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public string UpdatedByUserName { get; set; }
    }

    public class CreateImportFormTypesDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; }
        
        [Required]
        public string CreatedBy { get; set; }
    }

    public class UpdateImportFormTypesDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; }
        
        [Required]
        public string UpdatedBy { get; set; }
    }
}