using AutoMapper;
using Data.Models;
using Data.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace MoneyManager_API.MappingConfigs;

public class UserMappingConfig : Profile
{
    public UserMappingConfig()
    {
        this.CreateMap<UserDto, IdentityUser>().ReverseMap();
    }
}