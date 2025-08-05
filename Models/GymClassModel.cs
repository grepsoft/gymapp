

using System.ComponentModel.DataAnnotations;

namespace gymappyt.Models
{
    public enum MembershipType
    {
        None,
        Basic,
        Premium,
        Elite
    }

    public enum ClassType
    {
        Yoga,
        HIIT,
        Pilates,
        CrossFit,
        Kickboxing,
        StrengthTraining,
        FunctionalFitness,
        Cardio,
        Stretching
    }

    public enum DifficultyLevel
    {
        Beginner,
        Intermediate,
        Advanced
    }
        
    public class GymClassModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public ClassType Type { get; set; }

        [Required]
        public DifficultyLevel Level { get; set; }

        [Required]
        public MembershipType Membership { get; set; }

        [Required]
        public int InstructorId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, 50)]
        public int MaxCapacity { get; set; }

        [Range(0, 50)]
        public int CurrentBookings { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public InstructorModel Instructor { get; set; } = null!;
        public List<ClassBookingModel> Bookings { get; set; } = new();

        public TimeSpan Duration => EndTime - StartTime;

        public int AvailableSpots => MaxCapacity - CurrentBookings;

        public bool IsFull => CurrentBookings >= MaxCapacity;

        public string TypeDisplayName => Type switch
        {
            ClassType.Yoga => "Yoga",
            ClassType.HIIT => "HIIT",
            ClassType.Pilates => "Pilates",
            ClassType.CrossFit => "CrossFit",
            ClassType.Kickboxing => "Kickboxing",
            ClassType.StrengthTraining => "Strength Training",
            ClassType.FunctionalFitness => "Functional Fitness",
            ClassType.Cardio => "Cardio",
            ClassType.Stretching => "Stretching",
            _ => "Unknown"
        };

        public string LevelDisplayName => Level switch
        {
            DifficultyLevel.Beginner => "Beginner",
            DifficultyLevel.Intermediate => "Intermediate",
            DifficultyLevel.Advanced => "Advanced",
            _ => "Unknown"
        };

        public string GetIcon() => Type switch
        {
            ClassType.Yoga => "fas fa-leaf",
            ClassType.HIIT => "fas fa-fire",
            ClassType.Pilates => "fas fa-heart",
            ClassType.CrossFit => "fas fa-dumbbell",
            ClassType.Kickboxing => "fas fa-fist-raised",
            ClassType.StrengthTraining => "fas fa-weight-hanging",
            ClassType.FunctionalFitness => "fas fa-running",
            ClassType.Cardio => "fas fa-heartbeat",
            ClassType.Stretching => "fas fa-user-ninja",
            _ => "fas fa-calendar"
        };

        public string GetLevelColor() => Level switch
        {
            DifficultyLevel.Beginner => "bg-green-100 text-green-800",
            DifficultyLevel.Intermediate => "bg-yellow-100 text-yellow-800",
            DifficultyLevel.Advanced => "bg-red-100 text-red-800",
            _ => "bg-gray-100 text-gray-800"
        };
    }
}