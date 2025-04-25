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
            var errors = new Dictionary<string, string[]>
            {
                { "Null", new[] { "The model is null" } }
            };

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };

            return BadRequest(problemDetails);
        }
        if (!ModelState.IsValid)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "ModelState", new[] { "Invalid model" } }
            };

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };

            return BadRequest(problemDetails);
        }
        if (registerDTO.Password != registerDTO.ConfirmPassword)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Password", new[] { "Passwords do not match" } }
            };

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };

            return BadRequest(problemDetails);
        }
        if (await _userManager.FindByEmailAsync(registerDTO.Email) != null)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Email", new[] { "Email already exists" } }
            };

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };

            return BadRequest(problemDetails);
        }
        var newUser = UserMapper.MapToUser(registerDTO);
        newUser.UserName = newUser.Email;
        var result = await _userManager.CreateAsync(newUser, registerDTO.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.Select(e => e.Description).ToList());
        }
        if (result.Succeeded)
        {
            var role = await _userManager.AddToRoleAsync(newUser, "User");
            if (!role.Succeeded)
            {
                return BadRequest("User role assignment failed.");
            }
            var roleNewUser = await _userManager.GetRolesAsync(newUser);
            var token = await _tokenRepository.GenerateTokenAsync(newUser, roleNewUser.FirstOrDefault());
            return Ok(new { FullName = newUser.Names + " " + newUser.LastNames, Email = newUser.Email, Token = token });
        }
        return BadRequest("User creation failed.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (loginDTO == null)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Null", new[] { "The model is null" } }
            };

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };

            return BadRequest(problemDetails);
        }
        if (!ModelState.IsValid)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "ModelState", new[] { "Invalid model" } }
            };

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };

            return BadRequest(problemDetails);
        }
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "User or Password", new[] { "User or password is incorrect." } }
            };
            
            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };
            return BadRequest(problemDetails);
        }
        var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        if (!result)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "User or Password", new[] { "User or password is incorrect." } }
            };
            
            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            };
            return BadRequest(problemDetails);
        }
        var role = await _userManager.GetRolesAsync(user);
        var token = await _tokenRepository.GenerateTokenAsync(user, role.FirstOrDefault());
        return Ok(new { Token = token });
    }
}
