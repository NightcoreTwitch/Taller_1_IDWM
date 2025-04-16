using System.Threading.Tasks;
namespace Backend;

public interface IUserRepository
{
    Task<UserDTO> AddUserAsync(RegisterDTO user);
    Task<UserDTO> LoginAsync(LoginDTO user);
    Task<UserDTO> GetUserByEmailAsync(string email);
}
