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

    public class DeleteProductPricesRangeCommandValidator : AbstractValidator<DeleteProductPricesRangeCommand>
    {
        public DeleteProductPricesRangeCommandValidator(IProductPriceValidationService vs)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.PriceIds)
                .NotEmpty().WithMessage("At least one PriceId must be provided.")
                .MustAsync(async (ids, ct) =>
                {
                    foreach (var id in ids)
                        if (!await vs.PriceExistsByIdAsync(id, ct)) return false;
                    return true;
                })
                .WithMessage("One or more PriceIds do not exist.");
        }
    }


}
