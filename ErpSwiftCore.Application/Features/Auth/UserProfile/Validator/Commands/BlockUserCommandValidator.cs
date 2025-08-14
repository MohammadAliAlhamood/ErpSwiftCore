using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Dtos;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Commands
{

    /// <summary>
    /// Validator لـ BlockUserCommand: يتضمّن BlockUserRequestDto.
    /// </summary>
    public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.BlockUserRequest)
                .NotNull()
                .WithMessage("بيانات حظر المستخدم مطلوبة.")
                .SetValidator(new BlockUserRequestDtoValidator());
        }
    }
}
