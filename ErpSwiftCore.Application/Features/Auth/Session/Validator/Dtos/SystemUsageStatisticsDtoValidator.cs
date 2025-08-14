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
    /// Validator لـ SystemUsageStatisticsDto (للعرض فقط).
    /// </summary>
    public class SystemUsageStatisticsDtoValidator : AbstractValidator<SystemUsageStatisticsDto>
    {
        public SystemUsageStatisticsDtoValidator()
        {
            RuleFor(x => x.TotalUsers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("عدد المستخدمين الكلي يجب أن يكون صفر أو أكثر.");

            RuleFor(x => x.ActiveUsers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("عدد المستخدمين النشطين يجب أن يكون صفر أو أكثر.");

            RuleFor(x => x.BlockedUsers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("عدد المستخدمين المحظورين يجب أن يكون صفر أو أكثر.");

            RuleFor(x => x.TotalRoles)
                .GreaterThanOrEqualTo(0)
                .WithMessage("عدد الأدوار الكلي يجب أن يكون صفر أو أكثر.");
        }
    }
}
