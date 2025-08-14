using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    public class GetCategoryByNameQuery : IRequest<APIResponseDto>
    {
        public string Name { get; }

        public GetCategoryByNameQuery(string name)
        {
            Name = name;
        }
    }

}
