using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{
    public class UpdateOrderWithLinesCommand : IRequest<APIResponseDto>
    {
        public UpdateOrderWithLinesDto Dto { get; }
        public UpdateOrderWithLinesCommand(UpdateOrderWithLinesDto dto) => Dto = dto;
    }
}