using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Dtos
{
    /// <summary>
    /// Validator لـ UserDto (عادة لا يُستخدم في طلبات الـAPI بل للعرض فقط، لكن أدرجناه للتوافق الكامل).
    /// </summary>
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (Id) مطلوب.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress()
                .WithMessage("يجب أن يكون شكل البريد الإلكتروني صحيحاً.");

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
             
        } 
    }




  

  
}