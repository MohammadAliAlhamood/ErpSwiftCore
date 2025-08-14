using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Queries
{



    public class GetProductTaxesByIdsQueryValidator : AbstractValidator<GetProductTaxesByIdsQuery>
    {
        public GetProductTaxesByIdsQueryValidator(IProductTaxValidationService svc)
        {
            RuleFor(x => x.TaxIds)
                .NotNull().WithMessage("TaxIds must be provided.")
                .NotEmpty().WithMessage("At least one TaxId required.")
                .MustAsync(async (ids, ct) =>
                {
                    foreach (var id in ids)
                        if (!await svc.TaxExistsByIdAsync(id, ct)) return false;
                    return true;
                }).WithMessage("One or more TaxIds do not exist.");
        }
    }

}
