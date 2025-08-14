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

    public class SoftDeleteProductsRangeCommandValidator : AbstractValidator<SoftDeleteProductsRangeCommand>
    {
        public SoftDeleteProductsRangeCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Soft‐delete range payload is required.")
                .SetValidator(new ProductSoftDeleteRangeDtoValidator());
        }
    }



}
