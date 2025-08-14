using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{




    public class RestoreProductCommandValidator : AbstractValidator<RestoreProductCommand>
    {
        public RestoreProductCommandValidator(IProductValidationService vs)
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Restore payload is required.")
                .SetValidator(new ProductRestoreDtoValidator(vs));
        }
    }

}
