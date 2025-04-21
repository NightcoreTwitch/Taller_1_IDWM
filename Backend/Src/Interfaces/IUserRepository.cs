using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Src.Data;
using Backend.Src.DTOs;

namespace Backend.Src.Interfaces;

public interface IUserRepository
{
    Task<UserDTO> AddUserAsync(RegisterDTO user);
    Task<UserDTO> LoginAsync(LoginDTO user);
    Task<UserDTO> GetUserByEmailAsync(string email);
}
