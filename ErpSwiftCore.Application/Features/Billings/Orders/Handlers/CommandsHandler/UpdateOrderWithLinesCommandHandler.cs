using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Handlers.CommandsHandler
{
    public class UpdateOrderWithLinesCommandHandler : BaseHandler<UpdateOrderWithLinesCommand>
    {
        private readonly IOrderCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateOrderWithLinesCommandHandler(
            IOrderCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateOrderWithLinesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateOrderWithLinesCommand req, CancellationToken ct)
        {
            // خريطة UpdateOrderDto → Order
            var orderEntity = _mapper.Map<Order>(req.Dto.Order);

            // خريطة DTOs إلى OrderLine للسطور الجديدة
            var toAdd = (req.Dto.LinesToAdd ?? Enumerable.Empty<UpdateOrderLineDto>())
                .Select(dto => _mapper.Map<OrderLine>(dto))
                .ToList();

            // خريطة DTOs إلى OrderLine للسطور المعدّلة
            var toUpdate = (req.Dto.LinesToUpdate ?? Enumerable.Empty<UpdateOrderLineDto>())
                .Select(dto => _mapper.Map<OrderLine>(dto))
                .ToList();

            // استدعاء الخدمة
            var updated = await _svc.UpdateOrderWithLinesAsync(
                orderEntity,
                toAdd,
                toUpdate,
                req.Dto.LineIdsToDelete,
                ct
            );

            // (اختياري) إعادة خريطة updated → DTO
            return updated;
        }
    }
}