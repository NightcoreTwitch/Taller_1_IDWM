using System.ComponentModel.DataAnnotations;

namespace Backend.Src.DTOs;

public class RegisterDTO
{
    [Required(ErrorMessage = "Names is required.")]
    [MinLength(3, ErrorMessage = "Names must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "Names must be at most 50 characters long.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Names can only contain letters and spaces.")]
    public required string Names { get; set; }
    [Required(ErrorMessage = "Last names is required.")]
    [MinLength(3, ErrorMessage = "Last names must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "Last names must be at most 50 characters long.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last names can only contain letters and spaces.")]
    public required string LastNames { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Phone number is required.")]
    [MinLength(9, ErrorMessage = "Phone number must be at least 9 digits long.")]
    public required string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Birth date is required.")]
    public required DateOnly BirthDate { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(50, ErrorMessage = "Password must be at most 50 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public required string Password { get; set; }
    [Required(ErrorMessage = "Confirm password is required.")]
    [MinLength(8, ErrorMessage = "Confirm password must be at least 8 characters long.")]
    [MaxLength(50, ErrorMessage = "Confirm password must be at most 50 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Confirm password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public required string ConfirmPassword { get; set; }
}
