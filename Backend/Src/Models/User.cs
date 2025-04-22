using Microsoft.AspNetCore.Identity;

namespace Backend.Src.Models;

public class User : IdentityUser<int>
{
    public User()
    {
        SecurityStamp = Guid.NewGuid().ToString();
    }
    public required string Names { get; set; }
    public required string LastNames { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required string Password { get; set; }
    // Only Admins can view this field
    public string Status { get; set; } = "Active";
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly LastLogin { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    // Class Adress not needed on register form
    public Adress? Adress { get; set; }
}
