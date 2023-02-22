using Data.Models.DTO.Base;

namespace Data.Models.DTO;

public class UserDto : BaseDtoModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
}