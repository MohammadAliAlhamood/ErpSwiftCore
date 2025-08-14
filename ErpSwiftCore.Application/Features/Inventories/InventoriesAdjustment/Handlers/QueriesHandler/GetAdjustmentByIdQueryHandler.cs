using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Queries;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Handlers.QueriesHandler
{
    // 1. Get by ID
    public class GetAdjustmentByIdQueryHandler
        : BaseHandler<GetAdjustmentByIdQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentByIdQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentByIdQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetAdjustmentByIdAsync(req.AdjustmentId, ct);
            return _mapper.Map<InventoryAdjustmentDto?>(entity);
        }
    }

    // 2. Get all (non-deleted)
    public class GetAllAdjustmentsQueryHandler
        : BaseHandler<GetAllAdjustmentsQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllAdjustmentsQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllAdjustmentsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllAdjustmentsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAllAdjustmentsAsync(ct);
            return list.Select(e => _mapper.Map<InventoryAdjustmentDto>(e));
        }
    }

    // 3. Get all soft‑deleted
    public class GetAllSoftDeletedAdjustmentsQueryHandler
        : BaseHandler<GetAllSoftDeletedAdjustmentsQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllSoftDeletedAdjustmentsQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllSoftDeletedAdjustmentsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllSoftDeletedAdjustmentsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAllSoftDeletedAdjustmentsAsync(ct);
            return list.Select(e => _mapper.Map<InventoryAdjustmentDto>(e));
        }
    }

    // 4. Get soft‑deleted by ID
    public class GetSoftDeletedAdjustmentByIdQueryHandler
        : BaseHandler<GetSoftDeletedAdjustmentByIdQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetSoftDeletedAdjustmentByIdQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetSoftDeletedAdjustmentByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetSoftDeletedAdjustmentByIdQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetSoftDeletedAdjustmentByIdAsync(req.AdjustmentId, ct);
            return _mapper.Map<InventoryAdjustmentDto?>(entity);
        }
    }

    // 5. Get by multiple IDs
    public class GetAdjustmentsByIdsQueryHandler
        : BaseHandler<GetAdjustmentsByIdsQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentsByIdsQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentsByIdsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsByIdsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAdjustmentsByIdsAsync(req.AdjustmentIds, ct);
            return list.Select(e => _mapper.Map<InventoryAdjustmentDto>(e));
        }
    }

    // 6. Get by product (and optional warehouse)
    public class GetAdjustmentsByProductQueryHandler
        : BaseHandler<GetAdjustmentsByProductQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentsByProductQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentsByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsByProductQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAdjustmentsByProductAsync(req.ProductId, req.WarehouseId, ct);
            return list.Select(e => _mapper.Map<InventoryAdjustmentDto>(e));
        }
    }

    // 7. Get by warehouse
    public class GetAdjustmentsByWarehouseQueryHandler
        : BaseHandler<GetAdjustmentsByWarehouseQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentsByWarehouseQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentsByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsByWarehouseQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAdjustmentsByWarehouseAsync(req.WarehouseId, ct);
            return list.Select(e => _mapper.Map<InventoryAdjustmentDto>(e));
        }
    }

    // 8. Get by date‑range (and optional warehouse)
    public class GetAdjustmentsByDateRangeQueryHandler
        : BaseHandler<GetAdjustmentsByDateRangeQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentsByDateRangeQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentsByDateRangeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsByDateRangeQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAdjustmentsByDateRangeAsync(req.WarehouseId, req.From, req.To, ct);
            return list.Select(e => _mapper.Map<InventoryAdjustmentDto>(e));
        }
    }

    // 9. Get by stock‑take reference
    public class GetAdjustmentsByStockTakeIdQueryHandler
        : BaseHandler<GetAdjustmentsByStockTakeIdQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentsByStockTakeIdQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentsByStockTakeIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsByStockTakeIdQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAdjustmentsByStockTakeIdAsync(req.StockTakeId, ct);
            return list.Select(e => _mapper.Map<InventoryAdjustmentDto>(e));
        }
    }

    // 10. Get counts grouped by Reason
    public class GetAdjustmentCountsByReasonQueryHandler
        : BaseHandler<GetAdjustmentCountsByReasonQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;

        public GetAdjustmentCountsByReasonQueryHandler(
            IInventoryAdjustmentQueryService svc,
            ILogger<BaseHandler<GetAdjustmentCountsByReasonQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentCountsByReasonQuery req,
            CancellationToken ct)
        {
            var dict = await _svc.GetAdjustmentCountsByReasonAsync(req.WarehouseId, req.From, req.To, ct);
            return dict
                .Select(kv => new AdjustmentReasonCountDto { Reason = kv.Key, Count = kv.Value });
        }
    }

    // 11. Get total count
    public class GetAdjustmentsCountQueryHandler
        : BaseHandler<GetAdjustmentsCountQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;

        public GetAdjustmentsCountQueryHandler(
            IInventoryAdjustmentQueryService svc,
            ILogger<BaseHandler<GetAdjustmentsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsCountQuery req,
            CancellationToken ct)
            => Task.FromResult<object?>(await _svc.GetAdjustmentsCountAsync(ct));
    }

    // 12. Get count by Product
    public class GetAdjustmentsCountByProductQueryHandler
        : BaseHandler<GetAdjustmentsCountByProductQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;

        public GetAdjustmentsCountByProductQueryHandler(
            IInventoryAdjustmentQueryService svc,
            ILogger<BaseHandler<GetAdjustmentsCountByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsCountByProductQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetAdjustmentsCountByProductAsync(req.ProductId, ct);
            return count;
        }
    }

    // 13. Get count by Warehouse
    public class GetAdjustmentsCountByWarehouseQueryHandler
        : BaseHandler<GetAdjustmentsCountByWarehouseQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;

        public GetAdjustmentsCountByWarehouseQueryHandler(
            IInventoryAdjustmentQueryService svc,
            ILogger<BaseHandler<GetAdjustmentsCountByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentsCountByWarehouseQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetAdjustmentsCountByWarehouseAsync(req.WarehouseId, ct);
            return count;
        }
    }

    // 14. Get adjustment with Product included
    public class GetAdjustmentWithProductQueryHandler
        : BaseHandler<GetAdjustmentWithProductQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentWithProductQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentWithProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentWithProductQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetAdjustmentWithProductAsync(req.AdjustmentId, ct);
            return _mapper.Map<InventoryAdjustmentDto?>(entity);
        }
    }

    // 15. Get adjustment with Warehouse included
    public class GetAdjustmentWithWarehouseQueryHandler
        : BaseHandler<GetAdjustmentWithWarehouseQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetAdjustmentWithWarehouseQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAdjustmentWithWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAdjustmentWithWarehouseQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetAdjustmentWithWarehouseAsync(req.AdjustmentId, ct);
            return _mapper.Map<InventoryAdjustmentDto?>(entity);
        }
    }

    // 16. Get last adjustment for product + warehouse
    public class GetLastAdjustmentQueryHandler
        : BaseHandler<GetLastAdjustmentQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;
        private readonly IMapper _mapper;

        public GetLastAdjustmentQueryHandler(
            IInventoryAdjustmentQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetLastAdjustmentQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetLastAdjustmentQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetLastAdjustmentAsync(req.ProductId, req.WarehouseId, ct);
            return _mapper.Map<InventoryAdjustmentDto?>(entity);
        }
    }

    // 17. Get current stock after adjustments
    public class GetCurrentStockAfterAdjustmentsQueryHandler
        : BaseHandler<GetCurrentStockAfterAdjustmentsQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;

        public GetCurrentStockAfterAdjustmentsQueryHandler(
            IInventoryAdjustmentQueryService svc,
            ILogger<BaseHandler<GetCurrentStockAfterAdjustmentsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetCurrentStockAfterAdjustmentsQuery req,
            CancellationToken ct)
        {
            var stock = await _svc.GetCurrentStockAfterAdjustmentsAsync(req.ProductId, req.WarehouseId, ct);
            return stock;
        }
    }

    // 18. Sum quantity change by product within date‑range
    public class SumQuantityChangeByProductAndDateRangeQueryHandler
        : BaseHandler<SumQuantityChangeByProductAndDateRangeQuery>
    {
        private readonly IInventoryAdjustmentQueryService _svc;

        public SumQuantityChangeByProductAndDateRangeQueryHandler(
            IInventoryAdjustmentQueryService svc,
            ILogger<BaseHandler<SumQuantityChangeByProductAndDateRangeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            SumQuantityChangeByProductAndDateRangeQuery req,
            CancellationToken ct)
        {
            var sum = await _svc.SumQuantityChangeByProductAndDateRangeAsync(req.ProductId, req.From, req.To, ct);
            return sum;
        }
    }
}