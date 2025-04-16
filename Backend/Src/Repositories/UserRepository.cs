namespace Backend.Src.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<UserDTO> AddUserAsync(RegisterDTO user)
    {
        var newUser = UserMapper.MapToUser(user);
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return UserMapper.MapToUserDTO(newUser);
    }
    public async Task<UserDTO> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return UserMapper.MapToUserDTO(user);
    }
}