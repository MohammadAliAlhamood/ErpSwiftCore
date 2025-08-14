using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class SoftDeleteCategoriesRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CategoryIds { get; }

        public SoftDeleteCategoriesRangeCommand(IEnumerable<Guid> categoryIds)
        {
            CategoryIds = categoryIds;
        }
    }

}
