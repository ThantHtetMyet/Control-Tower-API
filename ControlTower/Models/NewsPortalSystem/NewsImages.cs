using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.NewsPortalSystem
{
    public class NewsImages
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("News")]
        public Guid NewsID { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public News News { get; set; }

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

        public bool IsFeatured { get; set; } = false;

        public bool IsDeleted { get; set; }

        public DateTime UploadedDate { get; set; }

        [ForeignKey("UploadedByUser")]
        public Guid? UploadedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? UploadedByUser { get; set; }
    }
}