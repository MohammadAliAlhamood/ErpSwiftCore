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
    public class GetProductTaxesPagedInactiveSoftDeletedQueryValidator : AbstractValidator<GetProductTaxesPagedInactiveSoftDeletedQuery>
    {
        public GetProductTaxesPagedInactiveSoftDeletedQueryValidator()
        {
            RuleFor(x => x.PageParams)
                .NotNull().WithMessage("Paging parameters required.")
                .SetValidator(new ProductTaxPageInactiveSoftDeletedDtoValidator());
        }
    }
}
