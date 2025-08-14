using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Dtos
{
    /// <summary>
    /// Validator لـ UpdateProfileRequestDto.
    /// </summary>
    public class UpdateProfileRequestDtoValidator : 
        AbstractValidator<UpdateProfileRequestDto>
    {
        public UpdateProfileRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (Id) مطلوب.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("الاسم مطلوب.");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .Must(p => string.IsNullOrEmpty(p) || System.Text.RegularExpressions.Regex.IsMatch(p, @"^\+?[0-9]{7,15}$"))
                .WithMessage("يجب أن يكون رقم الهاتف صالحاً (أرقام فقط أو مع رمز الدولة).");

            RuleFor(x => x.ProfilePictureUrl)
                .Cascade(CascadeMode.Stop)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("يجب أن يكون رابط الصورة في شكل URL صالح.");

            // Address يمكن أن تكون فارغة أو أي نص إضافي (لا حاجة لقواعد صعبة هنا)
        }
    }
}
