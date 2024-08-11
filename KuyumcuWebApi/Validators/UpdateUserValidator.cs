using FluentValidation;
using KuyumcuWebApi.dto;

namespace KuyumcuWebApi.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequestDto>
{

    public UpdateUserValidator()
    {
        RuleFor(item => item.FirstName).NotNull().NotEmpty().Length(3, 50);
        RuleFor(item => item.LastName).NotNull().NotEmpty().Length(3, 50);
        RuleFor(item => item.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(item => item.Phone).NotNull().NotEmpty().Length(10);
    }
}