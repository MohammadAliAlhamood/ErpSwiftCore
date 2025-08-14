using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Queries;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.Warehouses.Handlers.QueriesHandler
{
    // 1. Get all inventories in a warehouse
    public class GetInventoriesByWarehouseQueryHandler
        : BaseHandler<GetInventoriesByWarehouseQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetInventoriesByWarehouseQueryHandler(
            IWarehouseQueryService svc,
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
            return list.Select(i => _mapper.Map<WarehouseDto>(i));
        }
    }

    // 2. Get total inventory records count in a warehouse
    public class GetTotalInventoriesInWarehouseQueryHandler
        : BaseHandler<GetTotalInventoriesInWarehouseQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetTotalInventoriesInWarehouseQueryHandler(
            IWarehouseQueryService svc,
            ILogger<BaseHandler<GetTotalInventoriesInWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalInventoriesInWarehouseQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetTotalInventoriesInWarehouseAsync(req.WarehouseId, ct);
            return new WarehouseInventoriesCountDto
            {
                WarehouseID = req.WarehouseId,
                TotalInventories = count
            };
        }
    }

    // 3. Get total distinct products count in a warehouse
    public class GetTotalProductsInWarehouseQueryHandler
        : BaseHandler<GetTotalProductsInWarehouseQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetTotalProductsInWarehouseQueryHandler(
            IWarehouseQueryService svc,
            ILogger<BaseHandler<GetTotalProductsInWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalProductsInWarehouseQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetTotalProductsInWarehouseAsync(req.WarehouseId, ct);
            return new WarehouseDistinctProductsCountDto
            {
                WarehouseID = req.WarehouseId,
                TotalDistinctProducts = count
            };
        }
    }

    // 4. Get low-stock items count in a warehouse
    public class GetLowStockCountQueryHandler
        : BaseHandler<GetLowStockCountQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetLowStockCountQueryHandler(
            IWarehouseQueryService svc,
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
            return new WarehouseLowStockCountDto
            {
                WarehouseID = req.WarehouseId,
                LowStockCount = count
            };
        }
    }

    // 5. Get overstocked items count in a warehouse
    public class GetOverstockedCountQueryHandler
        : BaseHandler<GetOverstockedCountQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetOverstockedCountQueryHandler(
            IWarehouseQueryService svc,
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
            return new WarehouseOverstockCountDto
            {
                WarehouseID = req.WarehouseId,
                OverstockCount = count
            };
        }
    }

    // 6. Get average stock level in a warehouse
    public class GetAverageStockLevelQueryHandler
        : BaseHandler<GetAverageStockLevelQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetAverageStockLevelQueryHandler(
            IWarehouseQueryService svc,
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
            return new WarehouseAverageStockLevelDto
            {
                WarehouseID = req.WarehouseId,
                AverageStockLevel = avg
            };
        }
    }

    // 7. Get recent warehouses
    public class GetRecentWarehousesQueryHandler
        : BaseHandler<GetRecentWarehousesQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetRecentWarehousesQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetRecentWarehousesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetRecentWarehousesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetRecentWarehousesAsync(req.MaxCount, ct);
            return list.Select(w => _mapper.Map<RecentWarehouseDto>(w));
        }
    }

    // 8. Get inventory count per warehouse
    public class GetInventoryCountPerWarehouseQueryHandler
        : BaseHandler<GetInventoryCountPerWarehouseQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetInventoryCountPerWarehouseQueryHandler(
            IWarehouseQueryService svc,
            ILogger<BaseHandler<GetInventoryCountPerWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInventoryCountPerWarehouseQuery req,
            CancellationToken ct)
        {
            var dict = await _svc.GetInventoryCountPerWarehouseAsync(ct);
            return dict.Select(kv => new InventoryCountPerWarehouseDto
            {
                WarehouseID = kv.Key,
                InventoryCount = kv.Value
            });
        }
    }

    // 9. Get total number of warehouses
    public class GetWarehousesCountQueryHandler
        : BaseHandler<GetWarehousesCountQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetWarehousesCountQueryHandler(
            IWarehouseQueryService svc,
            ILogger<BaseHandler<GetWarehousesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetWarehousesCountQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetWarehousesCountAsync(ct);
            return new WarehouseCountDto { Count = count };
        }
    }

    // 10. Get warehouses count by branch
    public class GetWarehousesCountByBranchQueryHandler
        : BaseHandler<GetWarehousesCountByBranchQuery>
    {
        private readonly IWarehouseQueryService _svc;

        public GetWarehousesCountByBranchQueryHandler(
            IWarehouseQueryService svc,
            ILogger<BaseHandler<GetWarehousesCountByBranchQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetWarehousesCountByBranchQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetWarehousesCountByBranchAsync(req.BranchId, ct);
            return new WarehouseCountDto { Count = count };
        }
    }

    // 11. Get warehouse by ID
    public class GetWarehouseByIdQueryHandler
        : BaseHandler<GetWarehouseByIdQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetWarehouseByIdQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetWarehouseByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetWarehouseByIdQuery req,
            CancellationToken ct)
        {
            var w = await _svc.GetWarehouseByIdAsync(req.WarehouseId, ct);
            return _mapper.Map<WarehouseDto?>(w);
        }
    }

    // 12. Get soft-deleted warehouse by ID
    public class GetSoftDeletedWarehouseByIdQueryHandler
        : BaseHandler<GetSoftDeletedWarehouseByIdQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetSoftDeletedWarehouseByIdQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetSoftDeletedWarehouseByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetSoftDeletedWarehouseByIdQuery req,
            CancellationToken ct)
        {
            var w = await _svc.GetSoftDeletedWarehouseByIdAsync(req.WarehouseId, ct);
            return _mapper.Map<WarehouseDto?>(w);
        }
    }

    // 13. Get all warehouses
    public class GetAllWarehousesQueryHandler
        : BaseHandler<GetAllWarehousesQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllWarehousesQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllWarehousesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllWarehousesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAllWarehousesAsync(ct);
            return list.Select(w => _mapper.Map<WarehouseDto>(w));
        }
    }

    // 14. Get operational warehouses
    public class GetOperationalWarehousesQueryHandler
        : BaseHandler<GetOperationalWarehousesQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetOperationalWarehousesQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetOperationalWarehousesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetOperationalWarehousesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetOperationalWarehousesAsync(ct);
            return list.Select(w => _mapper.Map<WarehouseDto>(w));
        }
    }

    // 15. Get storage-only warehouses
    public class GetStorageOnlyWarehousesQueryHandler
        : BaseHandler<GetStorageOnlyWarehousesQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetStorageOnlyWarehousesQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStorageOnlyWarehousesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStorageOnlyWarehousesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStorageOnlyWarehousesAsync(ct);
            return list.Select(w => _mapper.Map<WarehouseDto>(w));
        }
    }

    // 16. Get warehouses by branch
    public class GetWarehousesByBranchQueryHandler
        : BaseHandler<GetWarehousesByBranchQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetWarehousesByBranchQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetWarehousesByBranchQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetWarehousesByBranchQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetWarehousesByBranchAsync(req.BranchId, ct);
            return list.Select(w => _mapper.Map<WarehouseDto>(w));
        }
    }

    // 17. Get warehouses by a list of IDs
    public class GetWarehousesByIdsQueryHandler
        : BaseHandler<GetWarehousesByIdsQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetWarehousesByIdsQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetWarehousesByIdsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetWarehousesByIdsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetWarehousesByIdsAsync(req.WarehouseIds, ct);
            return list.Select(w => _mapper.Map<WarehouseDto>(w));
        }
    }

    // 18. Get warehouse with branch included
    public class GetWarehouseWithBranchQueryHandler
        : BaseHandler<GetWarehouseWithBranchQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetWarehouseWithBranchQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetWarehouseWithBranchQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetWarehouseWithBranchQuery req,
            CancellationToken ct)
        {
            var w = await _svc.GetWarehouseWithBranchAsync(req.WarehouseId, ct);
            return _mapper.Map<WarehouseWithBranchDto?>(w);
        }
    }

    // 19. Get warehouse with inventories included
    public class GetWarehouseWithInventoriesQueryHandler
        : BaseHandler<GetWarehouseWithInventoriesQuery>
    {
        private readonly IWarehouseQueryService _svc;
        private readonly IMapper _mapper;

        public GetWarehouseWithInventoriesQueryHandler(
            IWarehouseQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetWarehouseWithInventoriesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetWarehouseWithInventoriesQuery req,
            CancellationToken ct)
        {
            var w = await _svc.GetWarehouseWithInventoriesAsync(req.WarehouseId, ct);
            return _mapper.Map<WarehouseWithInventoriesDto?>(w);
        }
    }
}


 