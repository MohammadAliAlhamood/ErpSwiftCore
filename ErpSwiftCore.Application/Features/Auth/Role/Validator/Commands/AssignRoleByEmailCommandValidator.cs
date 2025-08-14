using ErpSwiftCore.Application.Features.Auth.Role.Commands;
using ErpSwiftCore.Application.Features.Auth.Role.Validator.Dtos;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Auth.Role.Validator.Commands
{
    /// <summary>
    /// Validator لـ AssignRoleByEmailCommand.
    /// </summary>
    public class AssignRoleByEmailCommandValidator : AbstractValidator<AssignRoleByEmailCommand>
    {
        public AssignRoleByEmailCommandValidator()
        {
            RuleFor(x => x.AssignRoleRequest)
                .NotNull()
                .WithMessage("بيانات تعيين الدور بالبريد الإلكتروني مطلوبة.")
                .SetValidator(new AssignRoleByEmailRequestDtoValidator());
        }
    }
}
