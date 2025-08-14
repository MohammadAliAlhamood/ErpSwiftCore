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
    public class ProductTaxUpdateDtoValidator : AbstractValidator<ProductTaxUpdateDto>
    {
        public ProductTaxUpdateDtoValidator(IProductTaxValidationService validatorService)
        {
            Include(new ProductTaxCreateDtoValidator(validatorService));

            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("Tax ID is required.")
                .MustAsync((id, ct) => validatorService.TaxExistsByIdAsync(id, ct))
                .WithMessage("Tax with specified ID does not exist.");
        }
    }



}
