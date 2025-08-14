using ErpSwiftCore.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries
{
    public class GetProductTaxesPagedQuery : IRequest<APIResponseDto>
    {
        public BasePageParamDto PageParams { get; }

        public GetProductTaxesPagedQuery(BasePageParamDto pageParams)
        {
            PageParams = pageParams;
        }
    }

}
