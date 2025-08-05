
using System.ComponentModel.DataAnnotations;

namespace gymappyt.Models;

public class UserModel
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

    public string phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string password { get; set; } = string.Empty;

    [Compare("password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please accept the terms and conditions")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms and conditions")]
    public bool AcceptTerms { get; set; } = false;

    public bool SubscribeMarketing { get; set; } = false;

    // membership plan
    public MembershipType MembershipPlan { get; set; } = MembershipType.None;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;
}