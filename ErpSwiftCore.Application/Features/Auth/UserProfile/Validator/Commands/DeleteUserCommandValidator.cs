using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Commands
{

    /// <summary>
    /// Validator لـ DeleteUserCommand: يتضمّن DeleteUserRequestDto.
    /// </summary>
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.DeleteUserRequest)
                .NotNull()
                .WithMessage("بيانات حذف المستخدم مطلوبة.")
                .SetValidator(new DeleteUserRequestDtoValidator());
        }
    }
}
