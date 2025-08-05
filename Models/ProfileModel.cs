

using System.ComponentModel.DataAnnotations;
using gymappyt.Models;

public class ProfileModel
{

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;

    public MembershipType MembershipPlan { get; set; } = MembershipType.None;
}