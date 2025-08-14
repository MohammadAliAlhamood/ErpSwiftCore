using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress()
                .WithMessage("يجب أن يكون شكل البريد الإلكتروني صحيحاً.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("كلمة المرور مطلوبة.")
                .MinimumLength(6)
                .WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$")
                .WithMessage("كلمة المرور يجب أن تحتوي على حرف كبير وصغير ورقم وحرف خاص.");
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .Must(un => string.IsNullOrEmpty(un) || un.Length >= 3)
                .WithMessage("اسم المستخدم (UserName) إذا وُضع، فيجب أن يكون على الأقل 3 أحرف.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("الاسم (Name) للمستخدم مطلوب.");
            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .Must(p => string.IsNullOrEmpty(p) || System.Text.RegularExpressions.Regex.IsMatch(p, @"^\+?[0-9]{7,15}$"))
                .WithMessage("يجب أن يكون رقم الهاتف صالحاً (أرقام فقط أو مع رمز الدولة).");
            RuleFor(x => x.ProfilePictureUrl)
                .Cascade(CascadeMode.Stop)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("يجب أن يكون رابط الصورة في شكل URL صالح.");
            RuleFor(x => x.RoleName)
                .Cascade(CascadeMode.Stop)
                .Must(rn => string.IsNullOrEmpty(rn) || rn.Length >= 2)
                .WithMessage("اسم الدور (RoleName) إن وُجد، فيجب أن يكون على الأقل 2 أحرف.");
        }
    }
}
