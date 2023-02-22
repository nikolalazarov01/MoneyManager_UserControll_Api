using Data.Models;
using Data.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Utilities;

namespace Data.Repository.Contracts;

public interface IUserRepository
{
    Task<OperationResult> IsUnique(string username);
    Task<OperationResult<LoginResponseDto>> Login(string username, string password);
    Task<OperationResult<IdentityUser>> Register(IdentityUser user, string password);
    Task<OperationResult> Delete(Guid id);
    Task<OperationResult<IdentityUser>> GetById(Guid id);
}