using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.NewsPortalSystem
{
    public class NewsComments
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("News")]
        public Guid NewsID { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public News News { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User User { get; set; }

        public Guid? ParentCommentID { get; set; }
        
        [ForeignKey("ParentCommentID")]
        public NewsComments? ParentComment { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        public bool IsApproved { get; set; } = false;

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
        public virtual ICollection<NewsComments> Replies { get; set; } = new List<NewsComments>();
    }
}