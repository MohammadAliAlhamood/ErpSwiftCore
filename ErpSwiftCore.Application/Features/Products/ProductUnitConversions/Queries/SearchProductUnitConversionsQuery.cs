using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class SearchProductUnitConversionsQuery : IRequest<APIResponseDto>
    {
        public ProductUnitConversionSearchDto Dto { get; }

        public SearchProductUnitConversionsQuery(ProductUnitConversionSearchDto dto)
        {
            Dto = dto;
        }
    }



}
