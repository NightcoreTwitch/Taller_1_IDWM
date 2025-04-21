using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Mappers;

public class UserMapper
{
    public static User MapToUser(RegisterDTO registerDTO)
    {
        return new User
        {
            Names = registerDTO.Names,
            LastNames = registerDTO.LastNames,
            Email = registerDTO.Email,
            PhoneNumber = registerDTO.PhoneNumber,
            BirthDate = registerDTO.BirthDate,
            Password = registerDTO.Password
        };
    }
    public static User MapToUser(UserDTO userDTO)
    {
        return new User
        {
            Names = userDTO.Names,
            LastNames = userDTO.LastNames,
            Email = userDTO.Email,
            PhoneNumber = userDTO.PhoneNumber,
            BirthDate = userDTO.BirthDate,
            Password = userDTO.Password
        };
    }
    public static UserDTO MapToUserDTO(User user)
    {
        return new UserDTO
        {
            Names = user.Names,
            LastNames = user.LastNames,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            BirthDate = user.BirthDate,
            Password = user.Password
        };
    }
}
