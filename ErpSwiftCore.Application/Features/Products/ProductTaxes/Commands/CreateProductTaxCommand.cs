using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands
{
    public class CreateProductTaxCommand : IRequest<APIResponseDto>
    {
        public ProductTaxCreateDto Tax { get; }

        public CreateProductTaxCommand(ProductTaxCreateDto tax)
        {
            Tax = tax;
        }
    }
}