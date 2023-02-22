using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using Core.Contracts;
using Data.Models;
using Data.Models.DTO;
using Data.Models.DTO.Hateoas;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager_API.Configuration;
using MoneyManager_API.Extensions;
using Utilities;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MoneyManager_API.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _services;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterRequestDto> _validatorRegister;
    private readonly IValidator<LoginRequestDto> _validatorLogin;

    public UserController(IUnitOfWork services, IMapper mapper, IValidator<RegisterRequestDto> validatorRegister, IValidator<LoginRequestDto> validatorLogin)
    {
        _services = services;
        _mapper = mapper;
        _validatorRegister = validatorRegister;
        _validatorLogin = validatorLogin;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest, CancellationToken token)
    {
        var validationResult = await this.ValidateRegisterAsync(registerRequest, token);
        if (validationResult is { IsValid: false }) return this.ValidationError(validationResult);

        return await this.RegisterAsync(registerRequest, token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest, CancellationToken token)
    {
        var validationResult = await this.ValidateLoginAsync(loginRequest, token);
        if (validationResult is { IsValid: false }) return this.ValidationError(validationResult);

        return await this.LoginAsync(loginRequest, token);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        if (id == Guid.Empty) return BadRequest();

        var result = await this._services.Users.DeleteAsync(id);

        if (!result.IsSuccessful) return this.Error(result);

        return this.NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken token)
    {
        if (id == Guid.Empty) return BadRequest();

        var result = await _services.Users.GetById(id);
        if (!result.IsSuccessful) return this.Error(result);

        var representation = this._mapper.Map<UserDto>(result.Data);

        return Ok(representation);
    }

    private async Task<ValidationResult> ValidateRegisterAsync(RegisterRequestDto registerRequest, CancellationToken token)
    {
        if (this._validatorRegister is null) return null;
        return await this._validatorRegister.ValidateAsync(registerRequest, token);
    }

    private async Task<ValidationResult> ValidateLoginAsync(LoginRequestDto loginRequest, CancellationToken token)
    {
        if (this._validatorLogin is null) return null;
        return await this._validatorLogin.ValidateAsync(loginRequest, token);
    }

    private async Task<IActionResult> RegisterAsync(RegisterRequestDto registerRequestDto, CancellationToken token)
    {
        var result = await this._services.Users.RegisterAsync(registerRequestDto);
        if (!result.IsSuccessful) return this.Error(result);

        var representation = this._mapper.Map<UserDto>(result.Data);
        representation.Links = this.GetHateoasLinks(result.Data);
        
        return CreatedAtAction("GetById", new {Id = result.Data.Id}, representation);
    }

    private async Task<IActionResult> LoginAsync(LoginRequestDto loginRequest, CancellationToken token)
    {
        var result = await this._services.Users.LoginAsync(loginRequest);
        if (!result.IsSuccessful) return this.Error(result);

        return Ok(result.Data);
    }

    private IEnumerable<HateoasLink> GetHateoasLinks(IdentityUser user)
    {
        if (user is null) return Enumerable.Empty<HateoasLink>();

        var links = new List<HateoasLink>()
        {
            new()
            {
                Url = this.AbsoluteUrl("Login", "User", null), Method = HttpMethods.Post,
                Rel = "login"
            },
            new()
            {
                Url = this.AbsoluteUrl("Delete", "User", new { Id = user.Id }), Method = HttpMethods.Delete,
                Rel = "delete"
            },
            new()
            {
                Url = this.AbsoluteUrl("GetById", "User", new { Id = user.Id }), Method = HttpMethods.Get,
                Rel = "self"
            }
        };

        return links;
    }
}