using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlTower.Models.EmployeeManagementSystem
{
    public class UserImage
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("User")]
        [Required]
        public Guid UserID { get; set; }

        [ForeignKey("ImageType")]
        [Required]
        public Guid ImageTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageName { get; set; }

        [Required]
        [StringLength(500)]
        public string StoredDirectory { get; set; }

        [Required]
        [StringLength(50)]
        public string UploadedStatus { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime UploadedDate { get; set; }

        [ForeignKey("UploadedByUser")]
        [Required]
        public Guid UploadedBy { get; set; }

        // Add missing audit fields
        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ImageType ImageType { get; set; }
        public virtual User UploadedByUser { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}