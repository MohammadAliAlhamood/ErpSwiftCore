using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Queries
{
    public class GetProductUnitConversionsPagedSoftDeletedQueryValidator
      : AbstractValidator<GetProductUnitConversionsPagedSoftDeletedQuery>
    {
        public GetProductUnitConversionsPagedSoftDeletedQueryValidator(
            BasePageParamDtoValidator pageValidator)
        {
            RuleFor(x => x.Dto)
                .NotNull().WithMessage("Paging parameters are required.")
                .SetValidator(pageValidator);
        }
    }


}
