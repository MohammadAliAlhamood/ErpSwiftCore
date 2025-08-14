using ErpSwiftCore.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos
{

    public class BasePageParamDtoValidator
        : AbstractValidator<BasePageParamDto>
    {
        public BasePageParamDtoValidator()
        {
            RuleFor(x => x.pageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("pageIndex must be zero or greater.");

            RuleFor(x => x.pageSize)
                .GreaterThan(0)
                .WithMessage("pageSize must be greater than zero.");
        }
    }
}
