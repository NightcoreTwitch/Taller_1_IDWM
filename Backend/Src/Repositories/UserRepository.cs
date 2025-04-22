using Microsoft.EntityFrameworkCore;
using Backend.Src.Data;
using Backend.Src.DTOs;
using Backend.Src.Interfaces;
using Backend.Src.Mappers;
using Microsoft.AspNetCore.Identity;
using Backend.Src.Models;

namespace Backend.Src.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly ITokenRepository _tokenRepository;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    public UserRepository(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ITokenRepository tokenRepository)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenRepository = tokenRepository;
    }
    public async Task<string> AddUserAsync(RegisterDTO user)
    {
        var newUser = UserMapper.MapToUser(user);
        var result = await _userManager.CreateAsync(newUser, user.Password);
        if (!result.Succeeded)
        {
            return null;
        }

        var roleExists = await _roleManager.RoleExistsAsync("User");
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole<int>("User"));
        }

        await _userManager.AddToRoleAsync(newUser, "User");

        return await _tokenRepository.GenerateTokenAsync(newUser);
    }
    public async Task<string> LoginAsync(LoginDTO user)
    {
        var existingUser = await _userManager.FindByEmailAsync(user.Email);
        if (existingUser == null)
        {
            return null;
        }

        var result = await _userManager.CheckPasswordAsync(existingUser, user.Password);
        if (!result)
        {
            return null;
        }

        return await _tokenRepository.GenerateTokenAsync(existingUser);
    }
}