using Data.Models.DTO.Base;

namespace Data.Models.DTO;

public class LoginResponseDto : BaseDtoModel
{
    public string? UserName { get; set; }
    public string Token { get; set; }
}