using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class GetProductUnitConversionsPagedByProductQuery : IRequest<APIResponseDto>
    {
        public ProductUnitConversionPageByProductDto Dto { get; }

        public GetProductUnitConversionsPagedByProductQuery(ProductUnitConversionPageByProductDto dto)
        {
            Dto = dto;
        }
    }

}
