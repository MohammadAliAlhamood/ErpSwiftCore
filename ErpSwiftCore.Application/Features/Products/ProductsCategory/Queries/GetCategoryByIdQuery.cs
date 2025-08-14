using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries
{
    public class GetCategoryByIdQuery : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }

        public GetCategoryByIdQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    } 
}