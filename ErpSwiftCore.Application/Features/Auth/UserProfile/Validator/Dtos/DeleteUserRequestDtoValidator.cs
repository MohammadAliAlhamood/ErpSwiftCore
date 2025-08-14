using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using FluentValidation;  
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Dtos
{
    /// <summary>
    /// Validator لـ DeleteUserRequestDto.
    /// </summary>
    public class DeleteUserRequestDtoValidator : AbstractValidator<DeleteUserRequestDto>
    {
        public DeleteUserRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب للحذف.");
        }
    }
}
