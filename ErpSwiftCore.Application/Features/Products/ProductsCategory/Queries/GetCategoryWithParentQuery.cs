using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    // Relations
    public class GetCategoryWithParentQuery : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }

        public GetCategoryWithParentQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }

}
