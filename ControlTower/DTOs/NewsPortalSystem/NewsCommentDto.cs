using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.NewsPortalSystem
{
    public class CreateNewsCommentDto
    {
        [Required]
        public Guid NewsID { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid? ParentCommentID { get; set; }

        public string CreatedBy { get; set; }
    }

    public class UpdateNewsCommentDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Content { get; set; }

        public string? UpdatedBy { get; set; }
    }

    public class NewsCommentDto
    {
        public Guid ID { get; set; }
        public Guid NewsID { get; set; }
        public string? NewsTitle { get; set; }
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public Guid? ParentCommentID { get; set; }
        public string Content { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public int RepliesCount { get; set; }
        public List<NewsCommentDto> Replies { get; set; } = new List<NewsCommentDto>();
    }

    public class ApproveCommentDto
    {
        [Required]
        public Guid ID { get; set; }
        
        [Required]
        public bool IsApproved { get; set; }
        
        public string? UpdatedBy { get; set; }
    }
}