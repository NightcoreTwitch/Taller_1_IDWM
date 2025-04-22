using Backend.Src.DTOs;

namespace Backend.Src.Interfaces;

public interface IUserRepository
{
    Task<string> AddUserAsync(RegisterDTO user);
    Task<string> LoginAsync(LoginDTO user);
    Task<UserDTO> GetUserByEmailAsync(string email);
}
