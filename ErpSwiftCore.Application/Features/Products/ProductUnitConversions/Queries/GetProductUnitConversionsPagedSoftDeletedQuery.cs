using ErpSwiftCore.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class GetProductUnitConversionsPagedSoftDeletedQuery : IRequest<APIResponseDto>
    {
        public BasePageParamDto Dto { get; }
        public GetProductUnitConversionsPagedSoftDeletedQuery(BasePageParamDto dto)
        {
            Dto = dto;
        }
    }

}
