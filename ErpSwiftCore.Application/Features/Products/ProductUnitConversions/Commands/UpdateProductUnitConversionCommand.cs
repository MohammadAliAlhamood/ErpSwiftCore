using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands
{

    public class UpdateProductUnitConversionCommand : IRequest<APIResponseDto>
    {
        public ProductUnitConversionUpdateDto Dto { get; }

        public UpdateProductUnitConversionCommand(ProductUnitConversionUpdateDto dto)
        {
            Dto = dto;
        }
    }


}
