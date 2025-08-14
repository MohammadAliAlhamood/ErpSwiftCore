using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Queries
{
    public class GetPriceByIdQueryValidator : AbstractValidator<GetPriceByIdQuery>
    {
        public GetPriceByIdQueryValidator(IProductPriceValidationService vs)
        {
            RuleFor(x => x.PriceId)
                .NotEmpty().WithMessage("PriceId is required.")
                .MustAsync(vs.PriceExistsByIdAsync).WithMessage("Price record not found.");
        }
    }
  
   
    


}