using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("اسم المستخدم (UserName) مطلوب.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("كلمة المرور مطلوبة.");
        }
    }
}
