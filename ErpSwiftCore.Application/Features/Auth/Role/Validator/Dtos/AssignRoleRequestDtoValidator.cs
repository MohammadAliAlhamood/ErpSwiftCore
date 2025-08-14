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
    /// Validator لـ AssignRoleRequestDto.
    /// </summary>
    public class AssignRoleRequestDtoValidator : 
                 AbstractValidator<AssignRoleRequestDto>
    {
        public AssignRoleRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب.");

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage("اسم الدور (RoleName) مطلوب.")
                .MinimumLength(2)
                .WithMessage("اسم الدور يجب أن يكون على الأقل حرفين.");
        }
    }
}
