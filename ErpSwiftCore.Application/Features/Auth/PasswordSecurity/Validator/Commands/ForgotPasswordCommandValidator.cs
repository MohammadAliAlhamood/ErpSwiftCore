using ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands;
using ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Validator.Dtos;
using FluentValidation;

namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Validator.Commands
{
    /// <summary>
    /// Validator لـ ForgotPasswordCommand: يتضمّن ForgotPasswordRequestDto.
    /// </summary>
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.ForgotPasswordRequest)
                .NotNull()
                .WithMessage("بيانات نسيان كلمة المرور مطلوبة.")
                .SetValidator(new ForgotPasswordRequestDtoValidator());
        }
    }
    }
