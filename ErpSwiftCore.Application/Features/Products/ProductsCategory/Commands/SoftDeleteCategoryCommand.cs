using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class SoftDeleteCategoryCommand : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }

        public SoftDeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }




}
