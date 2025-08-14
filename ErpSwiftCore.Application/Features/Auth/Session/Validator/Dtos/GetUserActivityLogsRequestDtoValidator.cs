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
    /// Validator لـ GetUserActivityLogsRequestDto.
    /// </summary>
    public class GetUserActivityLogsRequestDtoValidator : 
        AbstractValidator<GetUserActivityLogsRequestDto>
    {
        public GetUserActivityLogsRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب لجلب السجلات.");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("تاريخ البداية (StartDate) مطلوب.");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("تاريخ النهاية (EndDate) مطلوب.");

            RuleFor(x => x)
                .Must(x => x.EndDate >= x.StartDate)
                .WithMessage("يجب أن يكون تاريخ النهاية (EndDate) أكبر من أو يساوي تاريخ البداية (StartDate).");
        }
    }
}
