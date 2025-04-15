namespace Backend;

public class Adress
{
    public int Id { get; set; }
    public required string Street { get; set; }
    public required string Number { get; set; }
    public required string City { get; set; }
    public required string Region { get; set; }
    public required string ZipCode { get; set; }
    // Foreign key to User
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
