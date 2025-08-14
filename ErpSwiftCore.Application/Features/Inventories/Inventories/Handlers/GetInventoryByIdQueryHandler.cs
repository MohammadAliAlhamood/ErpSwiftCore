using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.Inventories.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Inventories.Queries;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Dtos;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Inventories.Inventories.Handlers
{
    // 1. Get inventory by its ID
    public class GetInventoryByIdQueryHandler
        : BaseHandler<GetInventoryByIdQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryByIdQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryByIdQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInventoryByIdAsync(req.InventoryId, ct);
            return inv is null ? null : _mapper.Map<InventoryDto>(inv);
        }
    }

    // 2. Get soft‑deleted inventory by its ID
    public class GetSoftDeletedInventoryByIdQueryHandler
        : BaseHandler<GetSoftDeletedInventoryByIdQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetSoftDeletedInventoryByIdQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetSoftDeletedInventoryByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetSoftDeletedInventoryByIdQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetSoftDeletedInventoryByIdAsync(req.InventoryId, ct);
            return inv is null ? null : _mapper.Map<InventoryDto>(inv);
        }
    }

    // 3. Get inventory for a product in a specific warehouse
    public class GetInventoryByProductAndWarehouseQueryHandler
        : BaseHandler<GetInventoryByProductAndWarehouseQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryByProductAndWarehouseQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryByProductAndWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryByProductAndWarehouseQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInventoryByProductAndWarehouseAsync(req.ProductId, req.WarehouseId, ct);
            return inv is null ? null : _mapper.Map<InventoryDto>(inv);
        }
    }

    // 4. Get current inventory snapshot
    public class GetCurrentInventorySnapshotQueryHandler
        : BaseHandler<GetCurrentInventorySnapshotQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetCurrentInventorySnapshotQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetCurrentInventorySnapshotQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetCurrentInventorySnapshotQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetCurrentInventorySnapshotAsync(req.ProductId, req.WarehouseId, ct);
            return inv is null ? null : _mapper.Map<InventoryDto>(inv);
        }
    }

    // 5. Get last inventory record
    public class GetLastInventoryRecordQueryHandler
        : BaseHandler<GetLastInventoryRecordQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetLastInventoryRecordQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetLastInventoryRecordQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetLastInventoryRecordQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetLastInventoryRecordAsync(req.ProductId, req.WarehouseId, ct);
            return inv is null ? null : _mapper.Map<InventoryDto>(inv);
        }
    }

    // 6. Get all inventories
    public class GetAllInventoriesQueryHandler
        : BaseHandler<GetAllInventoriesQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllInventoriesQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllInventoriesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllInventoriesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAllInventoriesAsync(ct);
            return list.Select(i => _mapper.Map<InventoryDto>(i));
        }
    }

    // 7. Get inventories by product
    public class GetInventoriesByProductQueryHandler
        : BaseHandler<GetInventoriesByProductQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoriesByProductQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoriesByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoriesByProductQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInventoriesByProductAsync(req.ProductId, ct);
            return list.Select(i => _mapper.Map<InventoryDto>(i));
        }
    }

    // 8. Get inventories by warehouse
    public class GetInventoriesByWarehouseQueryHandler
        : BaseHandler<GetInventoriesByWarehouseQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoriesByWarehouseQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoriesByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoriesByWarehouseQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInventoriesByWarehouseAsync(req.WarehouseId, ct);
            return list.Select(i => _mapper.Map<InventoryDto>(i));
        }
    }




    // 10. Get inventories matching a filter expression
    public class GetInventoriesByFilterQueryHandler
        : BaseHandler<GetInventoriesByFilterQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoriesByFilterQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoriesByFilterQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoriesByFilterQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInventoriesByFilterAsync(req.Filter, ct);
            return list.Select(i => _mapper.Map<InventoryDto>(i));
        }
    }

    // 11. Get inventory with its product data
    public class GetInventoryWithProductQueryHandler
        : BaseHandler<GetInventoryWithProductQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryWithProductQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryWithProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryWithProductQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInventoryWithProductAsync(req.InventoryId, ct);
            return inv is null ? null : _mapper.Map<InventoryWithPolicyDto>(inv);
        }
    }

    // 12. Get inventory with its warehouse data
    public class GetInventoryWithWarehouseQueryHandler
        : BaseHandler<GetInventoryWithWarehouseQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryWithWarehouseQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryWithWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryWithWarehouseQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInventoryWithWarehouseAsync(req.InventoryId, ct);
            return inv is null ? null : _mapper.Map<InventoryWithPolicyDto>(inv);
        }
    }

    // 13. Get inventory with its policy
    public class GetInventoryWithPolicyQueryHandler
        : BaseHandler<GetInventoryWithPolicyQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryWithPolicyQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryWithPolicyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryWithPolicyQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInventoryWithPolicyAsync(req.InventoryId, ct);
            return inv is null ? null : _mapper.Map<InventoryWithPolicyDto>(inv);
        }
    }

    // 14. Get inventory with its transactions
    public class GetInventoryWithTransactionsQueryHandler
        : BaseHandler<GetInventoryWithTransactionsQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryWithTransactionsQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryWithTransactionsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryWithTransactionsQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInventoryWithTransactionsAsync(req.InventoryId, ct);
            return inv is null ? null : _mapper.Map<InventoryWithTransactionsDto>(inv);
        }
    }

    // 15. Get inventory with its notifications
    public class GetInventoryWithNotificationsQueryHandler
        : BaseHandler<GetInventoryWithNotificationsQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryWithNotificationsQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryWithNotificationsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryWithNotificationsQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInventoryWithNotificationsAsync(req.InventoryId, ct);
            return inv is null ? null : _mapper.Map<InventoryWithNotificationsDto>(inv);
        }
    }

    // 16. Get raw transactions for an inventory
    public class GetInventoryTransactionsQueryHandler
        : BaseHandler<GetInventoryTransactionsQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryTransactionsQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryTransactionsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryTransactionsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInventoryTransactionsAsync(req.InventoryId, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 17. Get policy for an inventory
    public class GetInventoryPolicyQueryHandler
        : BaseHandler<GetInventoryPolicyQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoryPolicyQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInventoryPolicyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryPolicyQuery req,
            CancellationToken ct)
        {
            var policy = await _svc.GetInventoryPolicyAsync(req.InventoryId, ct);
            return policy is null ? null : _mapper.Map<InventoryPolicyDto>(policy);
        }
    }

    // 18. Get total inventories count
    public class GetInventoriesCountQueryHandler
        : BaseHandler<GetInventoriesCountQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetInventoriesCountQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetInventoriesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoriesCountQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetInventoriesCountAsync(ct);
            return new InventoryCountDto { TotalInventories = count };
        }
    }

    // 19. Get inventories count by product
    public class GetInventoriesCountByProductQueryHandler
        : BaseHandler<GetInventoriesCountByProductQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetInventoriesCountByProductQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetInventoriesCountByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoriesCountByProductQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetInventoriesCountByProductAsync(req.ProductId, ct);
            return new InventoryCountByProductDto { ProductID = req.ProductId, Count = count };
        }
    }

    // 20. Get inventories count by warehouse
    public class GetInventoriesCountByWarehouseQueryHandler
        : BaseHandler<GetInventoriesCountByWarehouseQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetInventoriesCountByWarehouseQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetInventoriesCountByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoriesCountByWarehouseQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetInventoriesCountByWarehouseAsync(req.WarehouseId, ct);
            return new InventoryCountByWarehouseDto { WarehouseID = req.WarehouseId, Count = count };
        }
    }

    // 21. Get low‑stock count in a warehouse
    public class GetLowStockCountQueryHandler
        : BaseHandler<GetLowStockCountQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetLowStockCountQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetLowStockCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetLowStockCountQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetLowStockCountAsync(req.WarehouseId, ct);
            return new LowStockCountDto { WarehouseID = req.WarehouseId, CountBelowReorder = count };
        }
    }

    // 22. Get overstocked count in a warehouse
    public class GetOverstockedCountQueryHandler
        : BaseHandler<GetOverstockedCountQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetOverstockedCountQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetOverstockedCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetOverstockedCountQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetOverstockedCountAsync(req.WarehouseId, ct);
            return new OverstockCountDto { WarehouseID = req.WarehouseId, CountAboveMax = count };
        }
    }

    // 23. Get product availability across warehouses
    public class GetProductAvailabilityAcrossWarehousesQueryHandler
        : BaseHandler<GetProductAvailabilityAcrossWarehousesQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetProductAvailabilityAcrossWarehousesQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetProductAvailabilityAcrossWarehousesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetProductAvailabilityAcrossWarehousesQuery req,
            CancellationToken ct)
        {
            var dict = await _svc.GetProductAvailabilityAcrossWarehousesAsync(req.ProductId, ct);
            return new ProductAvailabilityDto { ProductID = req.ProductId, QuantityByWarehouse = dict };
        }
    }

    // 24. Get stock summary for multiple products
    public class GetStockSummaryByProductQueryHandler
        : BaseHandler<GetStockSummaryByProductQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetStockSummaryByProductQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetStockSummaryByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockSummaryByProductQuery req,
            CancellationToken ct)
        {
            var dict = await _svc.GetStockSummaryByProductAsync(req.ProductIds, ct);
            return new StockSummaryByProductDto { TotalAvailableByProduct = dict };
        }
    }

    // 25. Get total available quantity in a warehouse
    public class GetTotalAvailableQuantityByWarehouseQueryHandler
        : BaseHandler<GetTotalAvailableQuantityByWarehouseQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetTotalAvailableQuantityByWarehouseQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetTotalAvailableQuantityByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalAvailableQuantityByWarehouseQuery req,
            CancellationToken ct)
        {
            var total = await _svc.GetTotalAvailableQuantityByWarehouseAsync(req.WarehouseId, ct);
            return new TotalAvailableQuantityDto { WarehouseID = req.WarehouseId, TotalAvailable = total };
        }
    }

    // 26. Get total reserved quantity in a warehouse
    public class GetTotalReservedQuantityByWarehouseQueryHandler
        : BaseHandler<GetTotalReservedQuantityByWarehouseQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetTotalReservedQuantityByWarehouseQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetTotalReservedQuantityByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalReservedQuantityByWarehouseQuery req,
            CancellationToken ct)
        {
            var total = await _svc.GetTotalReservedQuantityByWarehouseAsync(req.WarehouseId, ct);
            return new TotalReservedQuantityDto { WarehouseID = req.WarehouseId, TotalReserved = total };
        }
    }

    // 27. Calculate inventory value in a warehouse
    public class CalculateInventoryValueQueryHandler
        : BaseHandler<CalculateInventoryValueQuery>
    {
        private readonly IInventoryQueryService _svc;

        public CalculateInventoryValueQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<CalculateInventoryValueQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            CalculateInventoryValueQuery req,
            CancellationToken ct)
        {
            var val = await _svc.CalculateInventoryValueAsync(req.WarehouseId, ct);
            return new InventoryValueDto { WarehouseID = req.WarehouseId, TotalValue = val };
        }
    }

    // 28. Get average stock level in a warehouse
    public class GetAverageStockLevelQueryHandler
        : BaseHandler<GetAverageStockLevelQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetAverageStockLevelQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetAverageStockLevelQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAverageStockLevelQuery req,
            CancellationToken ct)
        {
            var avg = await _svc.GetAverageStockLevelAsync(req.WarehouseId, ct);
            return new AverageStockLevelDto { WarehouseID = req.WarehouseId, AverageOnHand = avg };
        }
    }

    // 29. Get turnover rate for a product in a date range
    public class GetTurnoverRateQueryHandler
        : BaseHandler<GetTurnoverRateQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetTurnoverRateQueryHandler(
            IInventoryQueryService svc,
            ILogger<BaseHandler<GetTurnoverRateQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTurnoverRateQuery req,
            CancellationToken ct)
        {
            var rate = await _svc.GetTurnoverRateAsync(req.ProductId, req.From, req.To, ct);
            return new TurnoverRateDto { ProductID = req.ProductId, From = req.From, To = req.To, Rate = rate };
        }
    }

    // 30. Get current stock after adjustments
    public class GetCurrentStockAfterAdjustmentsQueryHandler
        : BaseHandler<GetCurrentStockAfterAdjustmentsQuery>
    {
        private readonly IInventoryQueryService _svc;

        public GetCurrentStockAfterAdjustmentsQueryHandler(
            IInventoryQueryService svc,
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
            return new CurrentStockAfterAdjustmentsDto
            {
                ProductID = req.ProductId,
                WarehouseID = req.WarehouseId,
                CurrentStock = stock
            };
        }
    }

    // 31. Get all inventories below reorder level
    public class GetBelowReorderLevelQueryHandler
        : BaseHandler<GetBelowReorderLevelQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetBelowReorderLevelQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetBelowReorderLevelQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetBelowReorderLevelQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetBelowReorderLevelAsync(ct);
            return list.Select(i => _mapper.Map<InventoryDto>(i));
        }
    }

    // 32. Get all inventories above max stock level
    public class GetAboveMaxStockLevelQueryHandler
        : BaseHandler<GetAboveMaxStockLevelQuery>
    {
        private readonly IInventoryQueryService _svc;
        private readonly IMapper _mapper;

        public GetAboveMaxStockLevelQueryHandler(
            IInventoryQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAboveMaxStockLevelQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAboveMaxStockLevelQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAboveMaxStockLevelAsync(ct);
            return list.Select(i => _mapper.Map<InventoryDto>(i));
        }
    }
}







