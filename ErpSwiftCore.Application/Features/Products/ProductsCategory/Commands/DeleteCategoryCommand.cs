using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class DeleteCategoryCommand : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }

        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
