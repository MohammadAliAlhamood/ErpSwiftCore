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
    /// Validator لـ ChangePasswordCommand: يتضمّن ChangePasswordRequestDto.
    /// </summary>
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.ChangePasswordRequest)
                .NotNull()
                .WithMessage("بيانات تغيير كلمة المرور مطلوبة.")
                .SetValidator(new ChangePasswordRequestDtoValidator());
        }
    }
}
