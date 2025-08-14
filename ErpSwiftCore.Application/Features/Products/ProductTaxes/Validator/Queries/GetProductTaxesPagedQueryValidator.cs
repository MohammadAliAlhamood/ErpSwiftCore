using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Queries
{
    // Generic validator for BasePagePaeamDto
    public class GetProductTaxesPagedQueryValidator : AbstractValidator<GetProductTaxesPagedQuery>
    {
        public GetProductTaxesPagedQueryValidator()
        {
            RuleFor(x => x.PageParams)
                .NotNull().WithMessage("Paging parameters required.")
                .SetValidator(new BasePageParamDtoValidator());
        }
    }
}
