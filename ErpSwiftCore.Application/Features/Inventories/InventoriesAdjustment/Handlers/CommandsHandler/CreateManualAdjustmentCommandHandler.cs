using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Commands;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Handlers.CommandsHandler
{
    // 1. Create manual adjustment
    public class CreateManualAdjustmentCommandHandler
        : BaseHandler<CreateManualAdjustmentCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;
        private readonly IMapper _mapper;

        public CreateManualAdjustmentCommandHandler(
            IInventoryAdjustmentCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<CreateManualAdjustmentCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            CreateManualAdjustmentCommand req,
            CancellationToken ct)
        {
            var dto = req.Dto;
            var id = await _svc.CreateManualAdjustmentAsync(
                dto.ProductId,
                dto.WarehouseId,
                dto.QuantityChanged,
                dto.Reason,
                ct);
            return new { Id = id };
        }
    }

    // 2. Delete single adjustment
    public class DeleteInventoryAdjustmentCommandHandler
        : BaseHandler<DeleteInventoryAdjustmentCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public DeleteInventoryAdjustmentCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<DeleteInventoryAdjustmentCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteInventoryAdjustmentCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAdjustmentAsync(req.Dto.AdjustmentId, ct);
            return new { Success = ok };
        }
    }

    // 3. Restore single adjustment
    public class RestoreInventoryAdjustmentCommandHandler
        : BaseHandler<RestoreInventoryAdjustmentCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public RestoreInventoryAdjustmentCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<RestoreInventoryAdjustmentCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreInventoryAdjustmentCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreAdjustmentAsync(req.Dto.AdjustmentId, ct);
            return new { Success = ok };
        }
    }

    // 4. Delete range of adjustments
    public class DeleteInventoryAdjustmentsRangeCommandHandler
        : BaseHandler<DeleteInventoryAdjustmentsRangeCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public DeleteInventoryAdjustmentsRangeCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<DeleteInventoryAdjustmentsRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteInventoryAdjustmentsRangeCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAdjustmentsRangeAsync(req.Dto.AdjustmentIds, ct);
            return new { Success = ok };
        }
    }

    // 5. Delete all adjustments
    public class DeleteAllInventoryAdjustmentsCommandHandler
        : BaseHandler<DeleteAllInventoryAdjustmentsCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public DeleteAllInventoryAdjustmentsCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<DeleteAllInventoryAdjustmentsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllInventoryAdjustmentsCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllAdjustmentsAsync(ct);
            return new { Success = ok };
        }
    }

    // 6. Restore range of adjustments
    public class RestoreInventoryAdjustmentsRangeCommandHandler
        : BaseHandler<RestoreInventoryAdjustmentsRangeCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public RestoreInventoryAdjustmentsRangeCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<RestoreInventoryAdjustmentsRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreInventoryAdjustmentsRangeCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreAdjustmentsRangeAsync(req.Dto.AdjustmentIds, ct);
            return new { Success = ok };
        }
    }

    // 7. Restore all adjustments
    public class RestoreAllInventoryAdjustmentsCommandHandler
        : BaseHandler<RestoreAllInventoryAdjustmentsCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public RestoreAllInventoryAdjustmentsCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<RestoreAllInventoryAdjustmentsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreAllInventoryAdjustmentsCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreAllAdjustmentsAsync(ct);
            return new { Success = ok };
        }
    }

    // 8. Bulk delete adjustments (return count)
    public class BulkDeleteInventoryAdjustmentsCommandHandler
        : BaseHandler<BulkDeleteInventoryAdjustmentsCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public BulkDeleteInventoryAdjustmentsCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<BulkDeleteInventoryAdjustmentsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkDeleteInventoryAdjustmentsCommand req,
            CancellationToken ct)
        {
            var count = await _svc.BulkDeleteAdjustmentsAsync(req.Dto.AdjustmentIds, ct);
            return new { DeletedCount = count };
        }
    }

    // 9. Update range of adjustments with deltas
    public class UpdateInventoryAdjustmentsRangeCommandHandler
        : BaseHandler<UpdateInventoryAdjustmentsRangeCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateInventoryAdjustmentsRangeCommandHandler(
            IInventoryAdjustmentCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateInventoryAdjustmentsRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateInventoryAdjustmentsRangeCommand req,
            CancellationToken ct)
        {
            // Map DTOs to domain entities
            var entities = req.Dto.Adjustments
                .Select(dto => _mapper.Map<InventoryAdjustment>(dto));
            var ok = await _svc.UpdateAdjustmentsAsync(entities, ct);
            return new { Success = ok };
        }
    }

    // 10. Update reason by date range
    public class UpdateInventoryAdjustmentReasonByDateRangeCommandHandler
        : BaseHandler<UpdateInventoryAdjustmentReasonByDateRangeCommand>
    {
        private readonly IInventoryAdjustmentCommandService _svc;

        public UpdateInventoryAdjustmentReasonByDateRangeCommandHandler(
            IInventoryAdjustmentCommandService svc,
            ILogger<BaseHandler<UpdateInventoryAdjustmentReasonByDateRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateInventoryAdjustmentReasonByDateRangeCommand req,
            CancellationToken ct)
        {
            var dto = req.Dto;
            var ok = await _svc.UpdateAdjustmentReasonByDateRangeAsync(
                dto.StartDate,
                dto.EndDate,
                dto.NewReason,
                ct);
            return new { Success = ok };
        }
    }
}