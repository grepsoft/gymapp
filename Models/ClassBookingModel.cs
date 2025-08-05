using System.ComponentModel.DataAnnotations;
using gymappyt.Models;

namespace gymappyt
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Waitlisted,
        Cancelled,
        Completed
    }

    public class ClassBookingModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

        public DateTime BookedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CancelledAt { get; set; }

        public string? CancellationReason { get; set; }

        public bool ShowedUp { get; set; } = false;

        // Navigation properties
        public UserModel User { get; set; } = null!;
        public GymClassModel Class { get; set; } = null!;

        public string StatusDisplayName => Status switch
        {
            BookingStatus.Confirmed => "Confirmed",
            BookingStatus.Cancelled => "Cancelled",
            BookingStatus.Waitlisted => "Waitlisted",
            BookingStatus.Completed => "Completed",
            _ => "Unknown"
        };

        public string GetStatusColor() => Status switch
        {
            BookingStatus.Confirmed => "bg-green-100 text-green-800",
            BookingStatus.Cancelled => "bg-red-100 text-red-800",
            BookingStatus.Waitlisted => "bg-yellow-100 text-yellow-800",
            BookingStatus.Completed => "bg-blue-100 text-blue-800",
            _ => "bg-gray-100 text-gray-800"
        };

        public bool CanCancel => Status == BookingStatus.Confirmed && Class.StartTime > DateTime.UtcNow.AddHours(2);
    }
}