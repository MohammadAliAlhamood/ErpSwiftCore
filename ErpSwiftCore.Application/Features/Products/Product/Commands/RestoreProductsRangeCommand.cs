using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{
    public class RestoreProductsRangeCommand : IRequest<APIResponseDto>
    {
        public ProductRestoreRangeDto Dto { get; }
        public RestoreProductsRangeCommand(ProductRestoreRangeDto dto) => Dto = dto;
    }

}
