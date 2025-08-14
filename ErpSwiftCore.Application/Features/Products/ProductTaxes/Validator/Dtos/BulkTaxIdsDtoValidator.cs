using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos
{
    public class BulkTaxIdsDtoValidator : AbstractValidator<BulkTaxIdsDto>
    {
        public BulkTaxIdsDtoValidator(IProductTaxValidationService validatorService)
        {
            RuleFor(x => x.TaxIds)
                .NotNull().WithMessage("TaxIds collection is required.")
                .NotEmpty().WithMessage("At least one TaxId must be provided.")
                .MustAsync(async (ids, ct) =>
                {
                    foreach (var id in ids)
                        if (!await validatorService.TaxExistsByIdAsync(id, ct))
                            return false;
                    return true;
                })
                .WithMessage("One or more TaxIds do not exist.");
        }
    }


}
