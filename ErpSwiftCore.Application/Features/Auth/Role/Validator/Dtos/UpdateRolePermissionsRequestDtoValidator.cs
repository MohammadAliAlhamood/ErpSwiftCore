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
    /// Validator لـ UpdateRolePermissionsRequestDto.
    /// </summary>
    public class UpdateRolePermissionsRequestDtoValidator : AbstractValidator<UpdateRolePermissionsRequestDto>
    {
        public UpdateRolePermissionsRequestDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage("اسم الدور (RoleName) مطلوب.")
                .MinimumLength(2)
                .WithMessage("اسم الدور يجب أن يكون على الأقل حرفين.");

            RuleFor(x => x.Permissions)
                .NotNull()
                .WithMessage("قائمة الصلاحيات (Permissions) لا يمكن أن تكون null.")
                .Must(perms => perms is IList<string> list && list.Count >= 0)
                .WithMessage("قائمة الصلاحيات يجب أن تكون قائمة (حتى لو كانت فارغة).");
        }
    }

}
