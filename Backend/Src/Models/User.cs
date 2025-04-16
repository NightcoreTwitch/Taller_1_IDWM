using System;

namespace Company.ClassLibrary1;

public class User
{
    public int Id { get; set; }
    public required string Names { get; set; }
    public required string LastNames { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required string Password { get; set; }
    // Only Admins can view this field
    public string Status { get; set; } = "Active";
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly LastLogin { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    // Class Adress not needed on register form
    public Adress? Adress { get; set; }
}
