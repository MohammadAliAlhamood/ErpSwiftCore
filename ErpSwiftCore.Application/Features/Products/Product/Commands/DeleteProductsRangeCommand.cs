using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{

    public class DeleteProductsRangeCommand : IRequest<APIResponseDto>
    {
        public ProductDeleteRangeDto Dto { get; }
        public DeleteProductsRangeCommand(ProductDeleteRangeDto dto) => Dto = dto;
    }


}
