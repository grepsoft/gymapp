
using System.ComponentModel.DataAnnotations;

namespace gymappyt.Models
{
    public enum Specialization
    {
        PersonalTraining,
        GroupFitness,
        Yoga,
        Pilates,
        CrossFit,
        Nutrition,
        Rehabilitation,
        WeightLoss,
        StrengthTraining,
        Cardio
    }

    public class InstructorModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [StringLength(500)]
        public string Bio { get; set; } = string.Empty;

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        public List<GymClassModel> Clases { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public string FullName => $"{FirstName} {LastName}";
    }
}

