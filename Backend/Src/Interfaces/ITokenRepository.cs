using Backend.Src.DTOs;

namespace Backend.Src.Interfaces;

public interface ITokenRepository
{
    Task<string> GenerateTokenAsync(UserDTO userDTO);
}
