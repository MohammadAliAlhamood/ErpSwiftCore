using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Session.Validator.Dtos
{
    /// <summary>
    /// Validator لـ SessionDto (للعرض فقط).
    /// </summary>
    public class SessionDtoValidator : AbstractValidator<SessionDto>
    {
        public SessionDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("معرّف الجلسة (Id) مطلوب.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) في الجلسة مطلوب.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty()
                .WithMessage("حقل CreatedAt لا يمكن أن يكون فارغاً.");

            RuleFor(x => x.ExpiresAt)
                .Cascade(CascadeMode.Stop)
                .Must(dt => dt == null || dt > DateTimeOffset.MinValue)
                .WithMessage("حقل ExpiresAt إن وُجد يجب أن يكون تاريخاً صالحاً.");
        }
    }
}
