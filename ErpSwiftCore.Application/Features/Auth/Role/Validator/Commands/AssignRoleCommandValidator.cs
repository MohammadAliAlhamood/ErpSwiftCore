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
    /// Validator لـ AssignRoleCommand: يتضمّن AssignRoleRequestDto.
    /// </summary>
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(x => x.AssignRoleRequest)
                .NotNull()
                .WithMessage("بيانات تعيين الدور مطلوبة.")
                .SetValidator(new AssignRoleRequestDtoValidator());
        }
    }
}
