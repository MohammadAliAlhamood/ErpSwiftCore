using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    public class GetCategoryWithSubCategoriesQuery : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }

        public GetCategoryWithSubCategoriesQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }


}
