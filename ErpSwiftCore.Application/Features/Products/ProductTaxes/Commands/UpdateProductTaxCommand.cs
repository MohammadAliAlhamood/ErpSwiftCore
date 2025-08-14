using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands
{
    public class UpdateProductTaxCommand : IRequest<APIResponseDto>
    {
        public ProductTaxUpdateDto Tax { get; }

        public UpdateProductTaxCommand(ProductTaxUpdateDto tax)
        {
            Tax = tax;
        }
    }



}
