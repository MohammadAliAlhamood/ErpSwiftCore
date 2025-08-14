using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Queries
{

    public class GetProductTaxesPagedSoftDeletedQueryValidator : AbstractValidator<GetProductTaxesPagedSoftDeletedQuery>
    {
        public GetProductTaxesPagedSoftDeletedQueryValidator()
        {
            RuleFor(x => x.PageParams)
                .NotNull().WithMessage("Paging parameters required.")
                .SetValidator(new ProductTaxPageSoftDeletedDtoValidator());
        }
    }
}
