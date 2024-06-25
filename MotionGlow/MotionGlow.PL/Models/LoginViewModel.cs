namespace MotionGlow.Models;
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    [RegularExpression(@"^(?=.*[0-9]).*$", ErrorMessage = "Password must contain at least one number.")]
    public string Password { get; set; }
}