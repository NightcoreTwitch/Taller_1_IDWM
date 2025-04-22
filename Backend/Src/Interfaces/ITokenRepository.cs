using Backend.Src.Models;

namespace Backend.Src.Interfaces;

public interface ITokenRepository
{
    Task<string> GenerateTokenAsync(User user);
}
