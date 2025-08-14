using ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands;
using ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Validator.Commands
{

    /// <summary>
    /// Validator لـ ResetPasswordCommand: يتضمّن ResetPasswordRequestDto.
    /// </summary>
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.ResetPasswordRequest)
                .NotNull()
                .WithMessage("بيانات إعادة تعيين كلمة المرور مطلوبة.")
                .SetValidator(new ResetPasswordRequestDtoValidator());
        }
    }
}
