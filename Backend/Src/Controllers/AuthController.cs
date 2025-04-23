using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Src.DTOs;
using Backend.Src.Interfaces;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Backend.Src.Mappers;

namespace Backend.Src.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public AuthController(IUserRepository userRepository, ITokenRepository tokenRepository, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if (registerDTO == null)
        {
            return BadRequest("Invalid data.");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (registerDTO.Password != registerDTO.ConfirmPassword)
        {
            return BadRequest("Passwords do not match.");
        }
        var newUser = UserMapper.MapToUser(registerDTO);
        newUser.UserName = newUser.Email;
        var result = await _userManager.CreateAsync(newUser, registerDTO.Password);
        if (!result.Succeeded)
        {
            return BadRequest("User creation failed.");
        }
        if (result.Succeeded)
        {
            var role = await _userManager.AddToRoleAsync(newUser, "User");
            if (!role.Succeeded)
            {
                return BadRequest("User role assignment failed.");
            }
            var token = await _tokenRepository.GenerateTokenAsync(newUser);
            return Ok(new { FullName = newUser.Names + " " + newUser.LastNames, Email = newUser.Email, Token = token });
        }
        return BadRequest("User creation failed.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (loginDTO == null)
        {
            return BadRequest("Invalid data.");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null)
        {
            return Unauthorized("User or password is incorrect.");
        }
        var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        if (!result)
        {
            return Unauthorized("User or password is incorrect.");
        }
        var token = await _tokenRepository.GenerateTokenAsync(user);
        return Ok(new { Token = token });
    }
}
