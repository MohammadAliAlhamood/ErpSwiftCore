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
    /// Validator لـ RoleDto (للعرض فقط).
    /// </summary>
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("اسم الدور (Name) لا يمكن أن يكون فارغاً.")
                .MinimumLength(2)
                .WithMessage("اسم الدور يجب أن يكون على الأقل حرفين.");
        }
    }

}
