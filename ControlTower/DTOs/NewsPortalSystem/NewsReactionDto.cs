using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.NewsPortalSystem
{
    public class CreateNewsReactionDto
    {
        [Required]
        public Guid NewsID { get; set; }

        [Required]
        [StringLength(50)]
        public string ReactionType { get; set; } // Like, Dislike, Love, Angry, Wow, etc.

        public string CreatedBy { get; set; }
    }

    public class NewsReactionDto
    {
        public Guid ID { get; set; }
        public Guid NewsID { get; set; }
        public string? NewsTitle { get; set; }
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string ReactionType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
    }

    public class NewsReactionSummaryDto
    {
        public Guid NewsID { get; set; }
        public Dictionary<string, int> ReactionCounts { get; set; } = new Dictionary<string, int>();
        public int TotalReactions { get; set; }
        public string? UserReaction { get; set; } // Current user's reaction if any
    }
}