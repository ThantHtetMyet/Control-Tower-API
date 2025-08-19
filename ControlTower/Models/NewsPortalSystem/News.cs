using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.NewsPortalSystem
{
    public class News
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? Excerpt { get; set; }

        [StringLength(255)]
        public string? Remark { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryID { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public NewsCategory NewsCategory { get; set; }

        public int? Rate { get; set; }

        public int ViewCount { get; set; } = 0;

        public DateTime? PublishDate { get; set; }

        public bool IsPublished { get; set; } = false;

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
        public virtual ICollection<NewsImages> NewsImages { get; set; } = new List<NewsImages>();
        public virtual ICollection<NewsComments> NewsComments { get; set; } = new List<NewsComments>();
        public virtual ICollection<NewsReactions> NewsReactions { get; set; } = new List<NewsReactions>();
    }
}