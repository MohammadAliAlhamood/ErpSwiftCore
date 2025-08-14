using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    public class GetCategoriesByParentQuery : IRequest<APIResponseDto>
    {
        public Guid ParentCategoryId { get; }

        public GetCategoriesByParentQuery(Guid parentCategoryId)
        {
            ParentCategoryId = parentCategoryId;
        }
    }
}
