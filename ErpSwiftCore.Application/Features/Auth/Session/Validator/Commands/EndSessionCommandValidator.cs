using ErpSwiftCore.Application.Features.Auth.Session.Commands;
using ErpSwiftCore.Application.Features.Auth.Session.Validator.Dtos; 
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Session.Validator.Commands
{
    /// <summary>
    /// Validator لـ EndSessionCommand: يتضمّن EndSessionRequestDto.
    /// </summary>
    public class EndSessionCommandValidator : AbstractValidator<EndSessionCommand>
    {
        public EndSessionCommandValidator()
        {
            RuleFor(x => x.EndSessionRequest)
                .NotNull()
                .WithMessage("بيانات إنهاء الجلسة مطلوبة.")
                .SetValidator(new EndSessionRequestDtoValidator());
        }
    }

}
