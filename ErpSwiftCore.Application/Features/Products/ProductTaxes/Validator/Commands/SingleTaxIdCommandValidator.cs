using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Commands
{
    public class SingleTaxIdCommandValidator<T> : AbstractValidator<T> where T : class
    {
        public SingleTaxIdCommandValidator(IProductTaxValidationService svc, System.Func<T, Guid> selector)
        {
            RuleFor(x => selector(x))
                .NotEmpty().WithMessage("TaxId is required.")
                .MustAsync(svc.TaxExistsByIdAsync).WithMessage("Tax does not exist.");
        }
    }
}
