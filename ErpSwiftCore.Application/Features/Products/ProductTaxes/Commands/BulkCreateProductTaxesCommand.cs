using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands
{
    public class BulkCreateProductTaxesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<ProductTaxCreateDto> Taxes { get; }

        public BulkCreateProductTaxesCommand(IEnumerable<ProductTaxCreateDto> taxes)
        {
            Taxes = taxes;
        }
    }


}
