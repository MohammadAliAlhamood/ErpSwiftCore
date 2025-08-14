using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    public class GetCategoriesCountByParentQuery : IRequest<APIResponseDto>
    {
        public Guid ParentCategoryId { get; }
        public bool Recursive { get; }

        public GetCategoriesCountByParentQuery(Guid parentCategoryId, bool recursive)
        {
            ParentCategoryId = parentCategoryId;
            Recursive = recursive;
        }
    }

}
