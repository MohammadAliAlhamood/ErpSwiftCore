using ErpSwiftCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Dtos
{
    /// <summary>
    /// تغيير حالة الطلب
    /// </summary>
    public class ChangeOrderStatusDto
    {
        public Guid OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
