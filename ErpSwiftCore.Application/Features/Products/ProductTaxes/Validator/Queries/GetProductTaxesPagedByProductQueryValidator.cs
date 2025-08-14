using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Queries
{
    public class GetProductTaxesPagedByProductQueryValidator : AbstractValidator<GetProductTaxesPagedByProductQuery>
    {
        public GetProductTaxesPagedByProductQueryValidator(IProductTaxValidationService svc)
        {
            RuleFor(x => x.PageParams)
                .NotNull().WithMessage("Paging parameters required.")
                .SetValidator(new ProductTaxPageByProductDtoValidator(svc));
        }
    }
}
