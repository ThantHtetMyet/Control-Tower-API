using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.NewsPortalSystem
{
    public class CreateNewsCategoryDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public Guid? ParentCategoryID { get; set; }

        public string CreatedBy { get; set; }
    }

    public class UpdateNewsCategoryDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public Guid? ParentCategoryID { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class NewsCategoryDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryID { get; set; }
        public string? ParentCategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public int NewsCount { get; set; }
        public int SubCategoriesCount { get; set; }
        public List<NewsCategoryDto> SubCategories { get; set; } = new List<NewsCategoryDto>();
    }

    public class NewsCategoryListDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public string? ParentCategoryName { get; set; }
        public Guid? ParentCategoryID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NewsCount { get; set; }
        public int SubCategoriesCount { get; set; }
    }
}