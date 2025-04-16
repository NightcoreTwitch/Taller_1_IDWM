namespace Backend.Src.Mappers;

public class UserMapper
{
    public static UserDTO => MapToUserDTO(RegisterDTO registerDTO)
    {
        return new UserDTO
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
}
