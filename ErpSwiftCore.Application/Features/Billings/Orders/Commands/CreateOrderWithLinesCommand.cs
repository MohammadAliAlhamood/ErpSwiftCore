using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{
    public class CreateOrderWithLinesCommand : IRequest<APIResponseDto>
    {
        public CreateOrderWithLinesDto Dto { get; }
        public CreateOrderWithLinesCommand(CreateOrderWithLinesDto dto) => Dto = dto;
    }
}