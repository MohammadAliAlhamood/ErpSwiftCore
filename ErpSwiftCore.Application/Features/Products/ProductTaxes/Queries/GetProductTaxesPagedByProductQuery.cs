using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries
{
    public class GetProductTaxesPagedByProductQuery : IRequest<APIResponseDto>
    {
        public ProductTaxPageByProductDto PageParams { get; }

        public GetProductTaxesPagedByProductQuery(ProductTaxPageByProductDto pageParams)
        {
            PageParams = pageParams;
        }
    }

}
