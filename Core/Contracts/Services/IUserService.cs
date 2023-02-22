using Data.Models;
using Data.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Utilities;

namespace Core.Contracts.Services;

public interface IUserService
{
    Task<OperationResult<IdentityUser>> RegisterAsync(RegisterRequestDto registerRequestDto);
    Task<OperationResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
    Task<OperationResult> DeleteAsync(Guid id);
    Task<OperationResult<IdentityUser>> GetById(Guid id);
}