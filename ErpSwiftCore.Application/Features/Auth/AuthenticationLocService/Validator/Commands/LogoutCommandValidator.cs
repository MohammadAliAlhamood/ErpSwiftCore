using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Commands;
using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Commands
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.LogoutRequest)
                .NotNull()
                .WithMessage("بيانات تسجيل الخروج مطلوبة.")
                .SetValidator(new LogoutRequestDtoValidator());
        }
    }

    public class LogoutAllSessionsCommandValidator : AbstractValidator<LogoutAllSessionsCommand>
    {
        public LogoutAllSessionsCommandValidator()
        {
            RuleFor(x => x.LogoutRequest)
                .NotNull()
                .WithMessage("بيانات تسجيل الخروج من جميع الجلسات مطلوبة.")
                .SetValidator(new LogoutRequestDtoValidator());
        }
    }
}