using Core.Contracts.Services;
using Data.Models;
using Data.Models.DTO;
using Data.Repository;
using Data.Repository.Contracts;
using Microsoft.AspNetCore.Identity;
using Utilities;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var operationResult = new OperationResult<LoginResponseDto>();
        try
        {
            var result = await this._repository.Login(loginRequestDto.Username, loginRequestDto.Password);

            return !result.IsSuccessful ? operationResult.AppendErrors(result) : operationResult.WithData(result.Data);
        }
        catch (Exception ex)
        {
            operationResult.AppendException(ex);
            return operationResult;
        }
    }

    public async Task<OperationResult> DeleteAsync(Guid id)
    {
        var operationResult = new OperationResult();

        try
        {
            var result = await _repository.Delete(id);
            if (!result.IsSuccessful) return operationResult.AppendErrors(result);

            return result;
        }
        catch (Exception ex)
        {
            operationResult.AppendException(ex);
            return operationResult;
        }
    }

    public async Task<OperationResult<IdentityUser>> GetById(Guid id)
    {
        var operationResult = new OperationResult<IdentityUser>();

        var result = await _repository.GetById(id);
        if (!result.IsSuccessful) return operationResult.AppendErrors(result);

        return operationResult.WithData(result.Data);
    }

    public async Task<OperationResult<IdentityUser>> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        var operationResult = new OperationResult<IdentityUser>();
        try
        {
            var isValid = await this._repository.IsUnique(registerRequestDto.UserName);
            if (!isValid.IsSuccessful)
            {
                return operationResult.AppendErrors(isValid);
            }

            IdentityUser user = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                NormalizedEmail = registerRequestDto.Email.ToUpper()
                //BaseCurrency = new Currency(registerRequestDto.Currency.Code)
            };

            var result = await this._repository.Register(user, registerRequestDto.Password);

            return !result.IsSuccessful ? operationResult.AppendErrors(result) : operationResult.WithData(result.Data);
        }
        catch (Exception ex)
        {
            operationResult.AppendException(ex);
            return operationResult;
        }
    }
}