using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos;
using ErpSwiftCore.Application.Features.Auth.Commands.UserProfile;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Commands
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            // نفّذ جميع قواعد التحقق على الـDTO الخاص بالتسجيل
            RuleFor(x => x.RegisterRequest)
                .NotNull()
                .WithMessage("بيانات التسجيل مطلوبة.")
                .SetValidator(new RegisterRequestDtoValidator());
        }
    } 
}


 