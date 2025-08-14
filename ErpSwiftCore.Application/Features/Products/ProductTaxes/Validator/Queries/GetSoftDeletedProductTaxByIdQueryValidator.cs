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

    public class GetSoftDeletedProductTaxByIdQueryValidator : AbstractValidator<GetSoftDeletedProductTaxByIdQuery>
    {
        public GetSoftDeletedProductTaxByIdQueryValidator(IProductTaxValidationService svc)
        {
            RuleFor(x => x.TaxId)
                .NotEmpty().WithMessage("TaxId is required.")
                .MustAsync(svc.TaxExistsByIdAsync).WithMessage("Tax does not exist.");
        }
    }
}
