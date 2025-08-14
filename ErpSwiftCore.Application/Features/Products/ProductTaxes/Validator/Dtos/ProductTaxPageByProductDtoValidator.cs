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
    public class ProductTaxPageByProductDtoValidator : AbstractValidator<ProductTaxPageByProductDto>
    {
        public ProductTaxPageByProductDtoValidator(IProductTaxValidationService validatorService)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync((id, ct) => validatorService.IsValidProductAsync(id, ct))
                .WithMessage("Product does not exist.");

            RuleFor(x => x.pageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("pageIndex must be zero or greater.");
            RuleFor(x => x.pageSize)
                .InclusiveBetween(1, 100).WithMessage("pageSize must be between 1 and 100.");
        }
    }

}
