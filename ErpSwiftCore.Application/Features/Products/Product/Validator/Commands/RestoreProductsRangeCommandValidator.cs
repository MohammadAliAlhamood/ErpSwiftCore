using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{


    public class RestoreProductsRangeCommandValidator : AbstractValidator<RestoreProductsRangeCommand>
    {
        public RestoreProductsRangeCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Restore range payload is required.")
                .SetValidator(new ProductRestoreRangeDtoValidator());
        }
    }


}
