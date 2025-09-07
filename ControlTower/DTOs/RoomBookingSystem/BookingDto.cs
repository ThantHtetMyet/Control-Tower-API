using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.RoomBookingSystem
{
    public class BookingDto
    {
        public Guid ID { get; set; }
        public Guid RoomID { get; set; }
        public string RoomName { get; set; }
        public string BuildingName { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid StatusID { get; set; }
        public string StatusName { get; set; }
        public Guid RequestedBy { get; set; }
        public string RequestedByName { get; set; }
        public Guid? ApprovedBy { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Guid? CancelledBy { get; set; }
        public string? CancelledByName { get; set; }
        public string? RejectionReason { get; set; }
        public string? CancellationReason { get; set; }
        public string? RecurrenceRule { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class CreateBookingDto
    {
        [Required]
        public Guid RoomID { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public Guid StatusID { get; set; }

        [StringLength(200)]
        public string? RecurrenceRule { get; set; }
    }

    public class UpdateBookingDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid RoomID { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public Guid StatusID { get; set; }

        [StringLength(200)]
        public string? RecurrenceRule { get; set; }
        
        // Add RowVersion property for optimistic concurrency control
        public byte[] RowVersion { get; set; }
    }

    public class ApproveBookingDto
    {
        [Required]
        public Guid ID { get; set; }
        
        // Add RowVersion property for optimistic concurrency control
        public byte[] RowVersion { get; set; }
    }

    public class RejectBookingDto
    {
        [Required]
        public Guid ID { get; set; }
    
        [Required]
        [StringLength(300)]
        public string RejectionReason { get; set; }
        
        // Add RowVersion property for optimistic concurrency control
        public byte[] RowVersion { get; set; }
    }

    public class CancelBookingDto
    {
        [Required]
        public Guid ID { get; set; }
    
        [Required]
        [StringLength(300)]
        public string CancellationReason { get; set; }
        
        // Add RowVersion property for optimistic concurrency control
        public byte[] RowVersion { get; set; }
    }

    public class CalendarBookingDto
    {
        public Guid ID { get; set; }
        public Guid RoomID { get; set; }
        public string RoomName { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StatusName { get; set; }
        public string RequestedByName { get; set; }
        public string? ApprovedByName { get; set; }
        public bool IsDeleted { get; set; }
    }
}