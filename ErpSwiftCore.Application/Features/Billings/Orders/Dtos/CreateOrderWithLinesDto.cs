using ErpSwiftCore.Domain.Entities.EntityBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Dtos
{
    public class CreateOrderWithLinesDto
    {
        public CreateOrderDto Order { get; set; } = default!;
        public IEnumerable<CreateOrderLineDto> OrderLines { get; set; } = default!;
    }
}