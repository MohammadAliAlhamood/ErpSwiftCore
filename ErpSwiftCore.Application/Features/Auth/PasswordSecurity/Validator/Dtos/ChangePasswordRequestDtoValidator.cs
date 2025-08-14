using ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Validator.Dtos
{
    /// <summary>
    /// Validator لـ ChangePasswordRequestDto.
    /// </summary>
    public class ChangePasswordRequestDtoValidator :
        AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordRequestDtoValidator()
        {
         
            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("كلمة المرور الحالية (CurrentPassword) مطلوبة.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("كلمة المرور الجديدة (NewPassword) مطلوبة.")
                .MinimumLength(6)
                .WithMessage("يجب أن تكون كلمة المرور الجديدة على الأقل 6 أحرف أو أرقام.")
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("كلمة المرور الجديدة يجب أن تختلف عن الحالية.");
        }
    }

}
