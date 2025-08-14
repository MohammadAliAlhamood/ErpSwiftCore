using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Validator.Dtos
{
    public class LogoutRequestDtoValidator : AbstractValidator<LogoutRequestDto>
    {
        public LogoutRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب.");
        }
    }














}