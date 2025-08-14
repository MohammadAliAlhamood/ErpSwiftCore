using ErpSwiftCore.Application.Features.Auth.Role.Commands;
using ErpSwiftCore.Application.Features.Auth.Role.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Role.Validator.Commands
{



    /// <summary>
    /// Validator لـ UpdateRolePermissionsCommand: يتضمّن UpdateRolePermissionsRequestDto.
    /// </summary>
    public class UpdateRolePermissionsCommandValidator : AbstractValidator<UpdateRolePermissionsCommand>
    {
        public UpdateRolePermissionsCommandValidator()
        {
            RuleFor(x => x.UpdateRolePermissionsRequest)
                .NotNull()
                .WithMessage("بيانات تحديث صلاحيات الدور مطلوبة.")
                .SetValidator(new UpdateRolePermissionsRequestDtoValidator());
        }
    }
}
