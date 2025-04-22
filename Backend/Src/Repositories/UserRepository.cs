using Microsoft.EntityFrameworkCore;
using Backend.Src.Data;
using Backend.Src.DTOs;
using Backend.Src.Interfaces;
using Backend.Src.Mappers;

namespace Backend.Src.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly ITokenRepository _tokenRepository;
    public UserRepository(DataContext context, ITokenRepository tokenRepository)
    {
        _context = context;
        _tokenRepository = tokenRepository;
    }
    public async Task<string> AddUserAsync(RegisterDTO user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
        {
            return null;
        }

        var newUser = UserMapper.MapToUser(user);
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return await _tokenRepository.GenerateTokenAsync(newUser);
    }
    public async Task<string> LoginAsync(LoginDTO user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
        if (existingUser == null)
        {
            return null;
        }

        return await _tokenRepository.GenerateTokenAsync(existingUser);
    }
    public async Task<UserDTO> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return null;
        }
        return UserMapper.MapToUserDTO(user);
    }
}