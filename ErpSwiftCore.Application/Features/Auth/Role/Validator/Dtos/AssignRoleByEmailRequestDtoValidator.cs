using ErpSwiftCore.Application.Features.Auth.Role.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Role.Validator.Dtos
{
    /// <summary>
    /// Validator لـ AssignRoleByEmailRequestDto.
    /// </summary>
    public class AssignRoleByEmailRequestDtoValidator : AbstractValidator<AssignRoleByEmailRequestDto>
    {
        public AssignRoleByEmailRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress()
                .WithMessage("صيغة البريد الإلكتروني غير صحيحة.");

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage("اسم الدور (RoleName) مطلوب.")
                .MinimumLength(2)
                .WithMessage("اسم الدور يجب أن يكون على الأقل حرفين.");
        }
    }
}
