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


    public class GetPricesByProductQueryValidator : AbstractValidator<GetPricesByProductQuery>
    {
        public GetPricesByProductQueryValidator(IProductPriceValidationService vs)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync(vs.IsValidProductAsync).WithMessage("Product does not exist or is deleted.");
        }
    }

}
