
using FluentValidation;
using KuyumcuWebApi.dto;

namespace KuyumcuWebApi.Validators;


public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(item => item.FirstName).NotNull().NotEmpty().Length(3, 50);
        RuleFor(item => item.LastName).NotNull().NotEmpty().Length(3, 50);
        RuleFor(item => item.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(item => item.Password).NotNull().NotEmpty().Length(3, 50);
        RuleFor(item => item.PasswordConfirm).NotNull().NotEmpty().Equal(el => el.Password).WithMessage("Parolalar eşleşmiyor");
        RuleFor(item => item.RoleId).NotNull().NotEmpty().Must(item => item == 1 || item == 2).WithMessage("RoleId 0 veya 1 olabilir");
    }
}