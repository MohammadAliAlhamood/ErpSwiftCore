using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands
{
    public class AddProductUnitConversionsRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<ProductUnitConversionCreateDto> Dtos { get; }

        public AddProductUnitConversionsRangeCommand(IEnumerable<ProductUnitConversionCreateDto> dtos)
        {
            Dtos = dtos;
        }
    }

}
