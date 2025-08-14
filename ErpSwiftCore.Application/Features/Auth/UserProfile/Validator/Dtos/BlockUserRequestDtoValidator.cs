using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Dtos
{
    /// <summary>
    /// Validator لـ BlockUserRequestDto.
    /// </summary>
    public class BlockUserRequestDtoValidator : AbstractValidator<BlockUserRequestDto>
    {
        public BlockUserRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب للحظر.");
        }
    }

}
