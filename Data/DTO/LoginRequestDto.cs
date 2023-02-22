using Data.Models.DTO.Base;

namespace Data.Models.DTO;

public class LoginRequestDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}