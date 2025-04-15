namespace Backend;

public class UserDTO
{
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Password { get; set; }
}
