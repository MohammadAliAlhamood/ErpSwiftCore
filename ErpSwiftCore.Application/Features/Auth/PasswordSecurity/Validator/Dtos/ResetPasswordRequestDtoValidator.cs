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
    /// Validator لـ ResetPasswordRequestDto.
    /// </summary>
    public class ResetPasswordRequestDtoValidator : 
        AbstractValidator<ResetPasswordRequestDto>
    {
        public ResetPasswordRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب.");

            RuleFor(x => x.ResetToken)
                .NotEmpty()
                .WithMessage("رمز إعادة التعيين (ResetToken) مطلوب.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("كلمة المرور الجديدة (NewPassword) مطلوبة.")
                .MinimumLength(6)
                .WithMessage("يجب أن تكون كلمة المرور الجديدة على الأقل 6 أحرف أو أرقام.");
        }
    }
}
