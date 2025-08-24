using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.NewsPortalSystem
{
    public class CreateNewsImageDto
    {
        [Required]
        public Guid NewsID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? StoredDirectory { get; set; }

        [StringLength(255)]
        public string? UploadedStatus { get; set; }

        [StringLength(255)]
        public string? AltText { get; set; }

        [StringLength(255)]
        public string? Caption { get; set; }

        [StringLength(255)]
        public string? ImageType { get; set; }

        public bool IsFeatured { get; set; } = false;

        public string UploadedBy { get; set; }
    }

    public class UpdateNewsImageDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? AltText { get; set; }

        [StringLength(255)]
        public string? Caption { get; set; }

        [StringLength(255)]
        public string? ImageType { get; set; }

        public bool IsFeatured { get; set; }
    }

    public class NewsImageDto
    {
        public Guid ID { get; set; }
        public Guid NewsID { get; set; }
        public string? NewsTitle { get; set; }
        public string Name { get; set; }
        public string? StoredDirectory { get; set; }
        public string? UploadedStatus { get; set; }
        public string? AltText { get; set; }
        public string? Caption { get; set; }
        public string? ImageType { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime UploadedDate { get; set; }
        public string? UploadedByUserName { get; set; }
        public string? ImageUrl { get; set; } // Computed property for full URL
    }

    // New DTOs for specific image types
    public class CreateThumbnailImageDto : CreateNewsImageDto
    {
        public CreateThumbnailImageDto()
        {
            ImageType = "thumbnail";
        }
    }

    public class CreateHeaderImageDto : CreateNewsImageDto
    {
        public CreateHeaderImageDto()
        {
            ImageType = "header";
        }
    }

    // New DTOs for file uploads that Swagger can handle
    public class UploadThumbnailDto
    {
        [Required]
        public IFormFile ThumbnailImage { get; set; }
        
        [Required]
        public string UploadedBy { get; set; }
    }

    public class UploadHeaderDto
    {
        [Required]
        public IFormFile HeaderImage { get; set; }
        
        [Required]
        public string UploadedBy { get; set; }
    }
}