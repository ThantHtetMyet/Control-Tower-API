using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.NewsPortalSystem
{
    public class NewsReactions
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

        [Required]
        [StringLength(50)]
        public string ReactionType { get; set; } // Like, Dislike, Love, Angry, Wow, etc.

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? CreatedByUser { get; set; }
    }
}