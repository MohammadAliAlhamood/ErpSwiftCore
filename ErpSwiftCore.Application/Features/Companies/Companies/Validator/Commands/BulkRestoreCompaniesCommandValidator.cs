using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Commands
{

    public class BulkRestoreCompaniesCommandValidator :
        AbstractValidator<BulkRestoreCompaniesCommand>
    {
        public BulkRestoreCompaniesCommandValidator()
        {
            RuleFor(x => x.CompanyIds)
                .NotNull().WithMessage("قائمة معرّفات الشركات لا يمكن أن تكون فارغة.")
                .Must(ids => ids != null && ids.All(id => id != Guid.Empty))
                    .WithMessage("جميع معرّفات الشركات يجب أن تكون صحيحة وغير فارغة.");
        }
    }


}
