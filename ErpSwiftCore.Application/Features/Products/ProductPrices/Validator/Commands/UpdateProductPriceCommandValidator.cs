using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Commands
{
    public class UpdateProductPriceCommandValidator : AbstractValidator<UpdateProductPriceCommand>
    {
        public UpdateProductPriceCommandValidator(IValidator<ProductPriceUpdateDto> dtoValidator)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Price).SetValidator(dtoValidator);
        }
    }

}
