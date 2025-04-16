using System.ComponentModel.DataAnnotations;

namespace Backend.Src.DTOs;

public class UserDTO
{
    [Required (ErrorMessage = "Names is required.")]
    [MinLength(3, ErrorMessage = "Names must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "Names must be at most 50 characters long.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Names can only contain letters and spaces.")]
    public string Names { get; set; }
    [Required (ErrorMessage = "Last names is required.")]
    [MinLength(3, ErrorMessage = "Last names must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "Last names must be at most 50 characters long.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last names can only contain letters and spaces.")]
    public string LastNames { get; set; }
    [Required (ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }
    [Required (ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    [RegularExpression(@"^\+?[0-9\s]+$", ErrorMessage = "Phone number can only contain numbers and spaces.")]
    [MinLength(9, ErrorMessage = "Phone number must be at least 9 digits long.")]
    public string PhoneNumber { get; set; }
    [Required (ErrorMessage = "Birth date is required.")]
    [MinLength(10, ErrorMessage = "Date must be format YYYY-MM-DD.")]
    [MaxLength(10, ErrorMessage = "Date must be format YYYY-MM-DD.")]
    public DateOnly BirthDate { get; set; }
    [Required (ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(50, ErrorMessage = "Password must be at most 50 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string Password { get; set; }
}
