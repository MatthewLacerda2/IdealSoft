namespace Backend.Models;
public class UserDto {
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public static explicit operator User(UserDto dto){
        return new User {
            Id = dto.Id,
            UserName = dto.UserName,
            Email = dto.Email,
            CreationDate = dto.CreationDate,

            NormalizedUserName = dto.UserName.ToUpper(),
            NormalizedEmail = dto.Email.ToUpper()
        };
    }

    public static explicit operator UserDto(User _user){
        return new UserDto {
            Id = _user.Id,
            UserName = _user.UserName!,
            Email = _user.Email!,
            CreationDate = _user.CreationDate
        };
    }
}