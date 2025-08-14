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
    /// Validator لـ ForgotPasswordRequestDto.
    /// </summary>
    public class ForgotPasswordRequestDtoValidator : 
        AbstractValidator<ForgotPasswordRequestDto>
    {
        public ForgotPasswordRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress()
                .WithMessage("يجب أن يكون شكل البريد الإلكتروني صحيحاً.");
        }
    }
}
