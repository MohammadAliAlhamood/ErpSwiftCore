using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos;
using ErpSwiftCore.Application.Features.Auth.Commands.Authentication;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Commands
{
    public class LoginCommandValidator : 
        AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.LoginRequest)
                .NotNull()
                .WithMessage("بيانات تسجيل الدخول مطلوبة.")
                .SetValidator(new LoginRequestDtoValidator());
        }
    }
}
