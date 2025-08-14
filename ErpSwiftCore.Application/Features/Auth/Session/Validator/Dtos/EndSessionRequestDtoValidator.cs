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
    /// Validator لـ EndSessionRequestDto.
    /// </summary>
    public class EndSessionRequestDtoValidator : AbstractValidator<EndSessionRequestDto>
    {
        public EndSessionRequestDtoValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty()
                .WithMessage("معرّف الجلسة (SessionId) مطلوب للإنهاء.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("يجب أن يكون معرّف الجلسة (SessionId) GUID صالحاً.");
        }
    }
}
