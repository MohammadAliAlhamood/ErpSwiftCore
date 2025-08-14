using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos
{
    public class LoginResponseDtoValidator :                AbstractValidator<LoginResponseDto>
    {
        public LoginResponseDtoValidator()
        {
            RuleFor(x => x.User)
                .NotNull()
                .WithMessage("بيانات المستخدم (User) لا يمكن أن تكون فارغة.");
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("الرمز (Token) المولّد لا يمكن أن يكون فارغاً.");
        }
    }
}
