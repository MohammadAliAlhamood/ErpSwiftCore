using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{
    public class SoftDeleteProductCommand : IRequest<APIResponseDto>
    {
        public ProductSoftDeleteDto Dto { get; }
        public SoftDeleteProductCommand(ProductSoftDeleteDto dto) => Dto = dto;
    }

}
