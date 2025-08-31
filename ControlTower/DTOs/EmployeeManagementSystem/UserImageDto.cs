using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.EmployeeManagementSystem
{
    public class UserImageDto
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid ImageTypeID { get; set; }
        public string ImageName { get; set; }
        public string StoredDirectory { get; set; }
        public string UploadedStatus { get; set; }
        public DateTime UploadedDate { get; set; }
        public Guid UploadedBy { get; set; }
        
        // Navigation properties for display
        public string? UserFullName { get; set; }
        public string? ImageTypeName { get; set; }
        public string? UploadedByName { get; set; }
    }

    public class CreateUserImageDto
    {
        [Required]
        public Guid UserID { get; set; }

        [Required]
        public Guid ImageTypeID { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public Guid UploadedBy { get; set; }
    }

    public class UpdateUserImageDto
    {
        [Required]
        public Guid ID { get; set; }

        public Guid? ImageTypeID { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required]
        public Guid UpdatedBy { get; set; }
    }
}