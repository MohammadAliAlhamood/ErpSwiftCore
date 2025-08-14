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
    public class BulkProductTaxCreateDtoValidator : AbstractValidator<BulkProductTaxCreateDto>
    {
        public BulkProductTaxCreateDtoValidator(IProductTaxValidationService validatorService)
        {
            RuleFor(x => x.Taxes)
                .NotNull().WithMessage("Taxes collection is required.")
                .NotEmpty().WithMessage("At least one tax must be provided.")
                .ForEach(t => t.SetValidator(new ProductTaxCreateDtoValidator(validatorService)));
        }
    }



}
