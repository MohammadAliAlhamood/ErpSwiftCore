using ErpSwiftCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{
    /// <summary>
    /// جلب الطلبات المصفاة حسب الحالة
    /// </summary>
    public class GetOrdersByStatusQuery : IRequest<APIResponseDto>
    {
        public OrderStatus Status { get; }
        public GetOrdersByStatusQuery(OrderStatus status) => Status = status;
    }
}