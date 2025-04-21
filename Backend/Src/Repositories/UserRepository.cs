using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Src.Data;
using Backend.Src.DTOs;
using Backend.Src.Interfaces;
using Backend.Src.Mappers;
using Backend.Src.Models;

namespace Backend.Src.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
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
    public async Task<UserDTO> LoginAsync(LoginDTO user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => 
            u.Email == user.Email &&
            u.Password == user.Password);
        if (existingUser == null)
        {
            return null;
        }
        return UserMapper.MapToUserDTO(existingUser);
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