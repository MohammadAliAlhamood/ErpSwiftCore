using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Dtos;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Commands
{
    /// <summary>
    /// Validator لـ UpdateProfileCommand: يتضمّن UpdateProfileRequestDto.
    /// </summary>
    public class UpdateProfileCommandValidator : 
        AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.UpdateProfileRequest)
                .NotNull()
                .WithMessage("بيانات تحديث الملف الشخصي مطلوبة.")
                .SetValidator(new UpdateProfileRequestDtoValidator());
        }
    }

}
