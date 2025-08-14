using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Commands
{


    public class RestoreProductPriceCommandValidator : AbstractValidator<RestoreProductPriceCommand>
    {
        public RestoreProductPriceCommandValidator(IProductPriceValidationService vs)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.PriceId)
                .NotEmpty().WithMessage("PriceId is required.")
                .MustAsync(vs.PriceExistsByIdAsync)
                .WithMessage("Price record not found.");
        }
    }


}
