using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    public class GetSoftDeletedCategoryByIdQuery : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }

        public GetSoftDeletedCategoryByIdQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }

}
