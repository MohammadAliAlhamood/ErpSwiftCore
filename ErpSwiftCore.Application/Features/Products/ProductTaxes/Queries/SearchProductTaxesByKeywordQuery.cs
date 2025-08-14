using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries
{
    public class SearchProductTaxesByKeywordQuery : IRequest<APIResponseDto>
    {
        public SearchProductTaxByKeywordDto Filter { get; }

        public SearchProductTaxesByKeywordQuery(SearchProductTaxByKeywordDto filter)
        {
            Filter = filter;
        }
    }

}
