using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos
{
    public class RegisterResponseDtoValidator : AbstractValidator<RegisterResponseDto>
    {
        public RegisterResponseDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("يجب أن يحتوي الرد على معرّف المستخدم (UserId).");
        }
    }
}
