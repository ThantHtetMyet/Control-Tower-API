using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.NewsPortalSystem
{
    public class NewsCategory
    {
        [Key]
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
        
        [ForeignKey("ParentCategoryID")]
        public NewsCategory? ParentCategory { get; set; }


        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? UpdatedByUser { get; set; }

        // Navigation properties
        public virtual ICollection<NewsCategory> SubCategories { get; set; } = new List<NewsCategory>();

        public virtual ICollection<News> News { get; set; } = new List<News>();
    }
}