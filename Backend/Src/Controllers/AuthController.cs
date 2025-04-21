using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Src.DTOs;
using Backend.Src.Interfaces;
using Backend.Src.Models;

namespace Backend.Src.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(registerDTO.Email);
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }
        if (registerDTO.Password != registerDTO.ConfirmPassword)
        {
            return BadRequest("Passwords do not match");
        }
        var user = await _userRepository.AddUserAsync(registerDTO);
        if (user == null)
        {
            return BadRequest("Error creating user");
        }
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var user = await _userRepository.LoginAsync(loginDTO);
        if (user == null)
        {
            return BadRequest("Invalid Email or Password");
        }
        return Ok(user);
    }
}
