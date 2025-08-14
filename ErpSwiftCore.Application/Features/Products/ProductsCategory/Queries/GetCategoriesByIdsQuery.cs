using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    public class GetCategoriesByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CategoryIds { get; }

        public GetCategoriesByIdsQuery(IEnumerable<Guid> categoryIds)
        {
            CategoryIds = categoryIds;
        }
    }

}
