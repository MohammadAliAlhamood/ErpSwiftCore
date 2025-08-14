using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class RestoreCategoryCommand : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }

        public RestoreCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }

}
