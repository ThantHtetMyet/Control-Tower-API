using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.NewsPortalSystem
{
    public class CreateNewsDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        public string? Description { get; set; }

        [StringLength(500)]
        public string? Excerpt { get; set; }

        [StringLength(255)]
        public string? Remark { get; set; }

        [Required]
        public Guid NewsCategoryID { get; set; }

        public int? Rate { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool IsPublished { get; set; } = false;

        public string CreatedBy { get; set; }
    }

    public class UpdateNewsDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        public string? Description { get; set; }

        [StringLength(500)]
        public string? Excerpt { get; set; }

        [StringLength(255)]
        public string? Remark { get; set; }

        [Required]
        public Guid NewsCategoryID { get; set; }

        public int? Rate { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class NewsDto
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public string? Excerpt { get; set; }
        public string? Remark { get; set; }
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public int? Rate { get; set; }
        public int ViewCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public int CommentsCount { get; set; }
        public int ReactionsCount { get; set; }
        public int ImagesCount { get; set; }
        public string FeaturedImageUrl { get; set; }
        public List<NewsImageDto> Images { get; set; } = new List<NewsImageDto>();
    }

    public class NewsListDto
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? Excerpt { get; set; }
        public Guid NewsCategoryID { get; set; }  // Add this missing property
        public string? CategoryName { get; set; }
        public int ViewCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public int CommentsCount { get; set; }
        public int ReactionsCount { get; set; }
        public string? FeaturedImageUrl { get; set; }
    }
}