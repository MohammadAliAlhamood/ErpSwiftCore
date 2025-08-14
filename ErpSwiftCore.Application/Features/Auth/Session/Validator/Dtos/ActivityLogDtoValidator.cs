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
    /// Validator لـ ActivityLogDto (للعرض فقط).
    /// </summary>
    public class ActivityLogDtoValidator : AbstractValidator<ActivityLogDto>
    {
        public ActivityLogDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("معرّف السجل (Id) مطلوب.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) في السجل مطلوب.");

            RuleFor(x => x.Timestamp)
                .NotEmpty()
                .WithMessage("التاريخ/الوقت (Timestamp) في السجل مطلوب.");


        }
    }

}
