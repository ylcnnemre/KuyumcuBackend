namespace KuyumcuWebApi.Validators;


using FluentValidation;
using KuyumcuWebApi.dto;

public class PersonValidator : AbstractValidator<UserLoginDto>
{
    public PersonValidator()
    {
        RuleFor(person => person.email).NotNull().EmailAddress();
        RuleFor(person => person.password).NotNull();
    }
}