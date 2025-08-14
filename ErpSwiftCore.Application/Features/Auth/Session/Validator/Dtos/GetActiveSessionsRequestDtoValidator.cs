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
    /// Validator لـ GetActiveSessionsRequestDto.
    /// </summary>
    public class GetActiveSessionsRequestDtoValidator :
        AbstractValidator<GetActiveSessionsRequestDto>
    {
        public GetActiveSessionsRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب لجلب الجلسات النشطة.");
        }
    }
}
