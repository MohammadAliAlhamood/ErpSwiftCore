using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Queries
{

    public class GetPricesByCurrencyQueryValidator : AbstractValidator<GetPricesByCurrencyQuery>
    {
        public GetPricesByCurrencyQueryValidator(IProductPriceValidationService vs)
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("CurrencyId is required.")
                .MustAsync(vs.IsValidCurrencyAsync).WithMessage("Currency does not exist or is deleted.");
        }
    }


}
