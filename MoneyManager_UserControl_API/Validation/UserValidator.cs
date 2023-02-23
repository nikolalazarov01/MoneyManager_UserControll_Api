using Data.Models.DTO;
using FluentValidation;

namespace MoneyManager_UserControll_APII.Validation;

public class UserValidator : AbstractValidator<RegisterRequestDto>
{
    public UserValidator()
    {
        this.RuleFor(u => u.UserName).NotEmpty();
        this.RuleFor(u => u.Password).NotEmpty();
        this.RuleFor(u => u.Email).NotEmpty();
        //this.RuleFor(u => u.Currency).NotEmpty();
    }
}

public class UserLoginValidator : AbstractValidator<LoginRequestDto>
{
    public UserLoginValidator()
    {
        this.RuleFor(u => u.Username).NotEmpty();
        this.RuleFor(u => u.Password).NotEmpty();
    }
}