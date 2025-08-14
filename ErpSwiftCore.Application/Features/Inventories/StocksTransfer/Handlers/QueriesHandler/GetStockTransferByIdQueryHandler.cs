using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Dtos;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Queries;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Handlers.QueriesHandler
{
    // 1. Get stock transfer by its ID
    public class GetStockTransferByIdQueryHandler
        : BaseHandler<GetStockTransferByIdQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransferByIdQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransferByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransferByIdQuery req,
            CancellationToken ct)
        {
            var t = await _svc.GetStockTransferByIdAsync(req.TransferId, ct);
            return t is null
                ? null
                : _mapper.Map<StockTransferDto>(t);
        }
    }

    // 2. Get soft‑deleted stock transfer by its ID
    public class GetSoftDeletedStockTransferByIdQueryHandler
        : BaseHandler<GetSoftDeletedStockTransferByIdQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetSoftDeletedStockTransferByIdQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetSoftDeletedStockTransferByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetSoftDeletedStockTransferByIdQuery req,
            CancellationToken ct)
        {
            var t = await _svc.GetSoftDeletedStockTransferByIdAsync(req.TransferId, ct);
            return t is null
                ? null
                : _mapper.Map<StockTransferDto>(t);
        }
    }

    // 3. Get all stock transfers
    public class GetAllStockTransfersQueryHandler
        : BaseHandler<GetAllStockTransfersQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllStockTransfersQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllStockTransfersQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllStockTransfersQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAllStockTransfersAsync(ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 4. Get active stock transfers
    public class GetActiveStockTransfersQueryHandler
        : BaseHandler<GetActiveStockTransfersQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetActiveStockTransfersQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetActiveStockTransfersQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetActiveStockTransfersQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetActiveStockTransfersAsync(ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 5. Get soft‑deleted stock transfers
    public class GetSoftDeletedStockTransfersQueryHandler
        : BaseHandler<GetSoftDeletedStockTransfersQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetSoftDeletedStockTransfersQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetSoftDeletedStockTransfersQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetSoftDeletedStockTransfersQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetSoftDeletedStockTransfersAsync(ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 6. Get stock transfers by IDs
    public class GetStockTransfersByIdsQueryHandler
        : BaseHandler<GetStockTransfersByIdsQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersByIdsQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersByIdsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersByIdsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStockTransfersByIdsAsync(req.TransferIds, ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 7. Get stock transfers by product
    public class GetStockTransfersByProductQueryHandler
        : BaseHandler<GetStockTransfersByProductQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersByProductQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersByProductQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStockTransfersByProductAsync(req.ProductId, ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 8. Get stock transfers by from‐warehouse
    public class GetStockTransfersByFromWarehouseQueryHandler
        : BaseHandler<GetStockTransfersByFromWarehouseQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersByFromWarehouseQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersByFromWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersByFromWarehouseQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStockTransfersByFromWarehouseAsync(req.FromWarehouseId, ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 9. Get stock transfers by to‐warehouse
    public class GetStockTransfersByToWarehouseQueryHandler
        : BaseHandler<GetStockTransfersByToWarehouseQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersByToWarehouseQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersByToWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersByToWarehouseQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStockTransfersByToWarehouseAsync(req.ToWarehouseId, ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 10. Get stock transfers by any warehouse
    public class GetStockTransfersByWarehouseQueryHandler
        : BaseHandler<GetStockTransfersByWarehouseQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersByWarehouseQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersByWarehouseQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStockTransfersByWarehouseAsync(req.WarehouseId, ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 11. Get stock transfers by date range
    public class GetStockTransfersByDateRangeQueryHandler
        : BaseHandler<GetStockTransfersByDateRangeQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersByDateRangeQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersByDateRangeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersByDateRangeQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStockTransfersByDateRangeAsync(req.From, req.To, ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 12. Get stock transfers by filter
    public class GetStockTransfersByFilterQueryHandler
        : BaseHandler<GetStockTransfersByFilterQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersByFilterQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersByFilterQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersByFilterQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetStockTransfersByFilterAsync(req.Filter, ct);
            return _mapper.Map<IReadOnlyList<StockTransferDto>>(list);
        }
    }

    // 13. Get paged stock transfers
    public class GetStockTransfersPagedQueryHandler
        : BaseHandler<GetStockTransfersPagedQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersPagedQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersPagedQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersPagedQuery req,
            CancellationToken ct)
        {
            var (items, total) = await _svc.GetStockTransfersPagedAsync(req.PageIndex, req.PageSize, ct);
            return new PagedStockTransferDto
            {
                Transfers = _mapper.Map<IReadOnlyList<StockTransferDto>>(items),
                TotalCount = total
            };
        }
    }

    // 14. Get paged by active status
    public class GetStockTransfersPagedByActiveStatusQueryHandler
        : BaseHandler<GetStockTransfersPagedByActiveStatusQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersPagedByActiveStatusQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersPagedByActiveStatusQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersPagedByActiveStatusQuery req,
            CancellationToken ct)
        {
            var (items, total) = await _svc.GetStockTransfersPagedByActiveStatusAsync(req.IsActive, req.PageIndex, req.PageSize, ct);
            return new PagedStockTransferDto
            {
                Transfers = _mapper.Map<IReadOnlyList<StockTransferDto>>(items),
                TotalCount = total
            };
        }
    }

    // 15. Get paged by product
    public class GetStockTransfersPagedByProductQueryHandler
        : BaseHandler<GetStockTransfersPagedByProductQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersPagedByProductQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersPagedByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersPagedByProductQuery req,
            CancellationToken ct)
        {
            var (items, total) = await _svc.GetStockTransfersPagedByProductAsync(req.ProductId, req.PageIndex, req.PageSize, ct);
            return new PagedStockTransferDto
            {
                Transfers = _mapper.Map<IReadOnlyList<StockTransferDto>>(items),
                TotalCount = total
            };
        }
    }

    // 16. Get paged by warehouse
    public class GetStockTransfersPagedByWarehouseQueryHandler
        : BaseHandler<GetStockTransfersPagedByWarehouseQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransfersPagedByWarehouseQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransfersPagedByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersPagedByWarehouseQuery req,
            CancellationToken ct)
        {
            var (items, total) = await _svc.GetStockTransfersPagedByWarehouseAsync(req.WarehouseId, req.PageIndex, req.PageSize, ct);
            return new PagedStockTransferDto
            {
                Transfers = _mapper.Map<IReadOnlyList<StockTransferDto>>(items),
                TotalCount = total
            };
        }
    }

    // 17. Get transfer with product details
    public class GetStockTransferWithProductQueryHandler
        : BaseHandler<GetStockTransferWithProductQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransferWithProductQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransferWithProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransferWithProductQuery req,
            CancellationToken ct)
        {
            var t = await _svc.GetStockTransferWithProductAsync(req.TransferId, ct);
            return t is null
                ? null
                : _mapper.Map<StockTransferWithProductDto>(t);
        }
    }

    // 18. Get transfer with warehouses details
    public class GetStockTransferWithWarehousesQueryHandler
        : BaseHandler<GetStockTransferWithWarehousesQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetStockTransferWithWarehousesQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetStockTransferWithWarehousesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransferWithWarehousesQuery req,
            CancellationToken ct)
        {
            var t = await _svc.GetStockTransferWithWarehousesAsync(req.TransferId, ct);
            return t is null
                ? null
                : _mapper.Map<StockTransferWithWarehousesDto>(t);
        }
    }

    // 19. Get total count of stock transfers
    public class GetStockTransfersCountQueryHandler
        : BaseHandler<GetStockTransfersCountQuery>
    {
        private readonly IStockTransferQueryService _svc;

        public GetStockTransfersCountQueryHandler(
            IStockTransferQueryService svc,
            ILogger<BaseHandler<GetStockTransfersCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetStockTransfersCountAsync(ct);
            return new StockTransferCountDto { TotalTransfers = cnt };
        }
    }

    // 20. Get count by product
    public class GetStockTransfersCountByProductQueryHandler
        : BaseHandler<GetStockTransfersCountByProductQuery>
    {
        private readonly IStockTransferQueryService _svc;

        public GetStockTransfersCountByProductQueryHandler(
            IStockTransferQueryService svc,
            ILogger<BaseHandler<GetStockTransfersCountByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersCountByProductQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetStockTransfersCountByProductAsync(req.ProductId, ct);
            return new StockTransferCountByProductDto
            {
                ProductID = req.ProductId,
                Count = cnt
            };
        }
    }

    // 21. Get count by warehouse
    public class GetStockTransfersCountByWarehouseQueryHandler
        : BaseHandler<GetStockTransfersCountByWarehouseQuery>
    {
        private readonly IStockTransferQueryService _svc;

        public GetStockTransfersCountByWarehouseQueryHandler(
            IStockTransferQueryService svc,
            ILogger<BaseHandler<GetStockTransfersCountByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetStockTransfersCountByWarehouseQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetStockTransfersCountByWarehouseAsync(req.WarehouseId, ct);
            return new StockTransferCountByWarehouseDto
            {
                WarehouseID = req.WarehouseId,
                Count = cnt
            };
        }
    }

    // 22. Get total transferred quantity by product
    public class GetTotalTransferredQuantityByProductQueryHandler
        : BaseHandler<GetTotalTransferredQuantityByProductQuery>
    {
        private readonly IStockTransferQueryService _svc;

        public GetTotalTransferredQuantityByProductQueryHandler(
            IStockTransferQueryService svc,
            ILogger<BaseHandler<GetTotalTransferredQuantityByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalTransferredQuantityByProductQuery req,
            CancellationToken ct)
        {
            var qty = await _svc.GetTotalTransferredQuantityByProductAsync(req.ProductId, req.From, req.To, ct);
            return new TotalTransferredQuantityByProductDto
            {
                ProductID = req.ProductId,
                From = req.From,
                To = req.To,
                TotalQuantity = qty
            };
        }
    }

    // 23. Get total transferred quantity by warehouse
    public class GetTotalTransferredQuantityByWarehouseQueryHandler
        : BaseHandler<GetTotalTransferredQuantityByWarehouseQuery>
    {
        private readonly IStockTransferQueryService _svc;

        public GetTotalTransferredQuantityByWarehouseQueryHandler(
            IStockTransferQueryService svc,
            ILogger<BaseHandler<GetTotalTransferredQuantityByWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalTransferredQuantityByWarehouseQuery req,
            CancellationToken ct)
        {
            var qty = await _svc.GetTotalTransferredQuantityByWarehouseAsync(req.WarehouseId, req.From, req.To, ct);
            return new TotalTransferredQuantityByWarehouseDto
            {
                WarehouseID = req.WarehouseId,
                From = req.From,
                To = req.To,
                TotalQuantity = qty
            };
        }
    }

    // 24. Search by notes
    public class SearchStockTransfersByNotesQueryHandler
        : BaseHandler<SearchStockTransfersByNotesQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public SearchStockTransfersByNotesQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<SearchStockTransfersByNotesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            SearchStockTransfersByNotesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.SearchStockTransfersByNotesAsync(req.NoteTerm, ct);
            return new StockTransferSearchResultsDto
            {
                Items = _mapper.Map<IReadOnlyList<StockTransferDto>>(list),
                TotalCount = list.Count
            };
        }
    }

    // 25. Search by reference
    public class SearchStockTransfersByReferenceQueryHandler
        : BaseHandler<SearchStockTransfersByReferenceQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public SearchStockTransfersByReferenceQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<SearchStockTransfersByReferenceQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            SearchStockTransfersByReferenceQuery req,
            CancellationToken ct)
        {
            var list = await _svc.SearchStockTransfersByReferenceAsync(req.ReferenceNumber, ct);
            return new StockTransferSearchResultsDto
            {
                Items = _mapper.Map<IReadOnlyList<StockTransferDto>>(list),
                TotalCount = list.Count
            };
        }
    }

    // 26. Search by product name
    public class SearchStockTransfersByProductNameQueryHandler
        : BaseHandler<SearchStockTransfersByProductNameQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public SearchStockTransfersByProductNameQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<SearchStockTransfersByProductNameQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            SearchStockTransfersByProductNameQuery req,
            CancellationToken ct)
        {
            var list = await _svc.SearchStockTransfersByProductNameAsync(req.ProductName, ct);
            return new StockTransferSearchResultsDto
            {
                Items = _mapper.Map<IReadOnlyList<StockTransferDto>>(list),
                TotalCount = list.Count
            };
        }
    }

    // 27. Get last transfer for product
    public class GetLastStockTransferForProductQueryHandler
        : BaseHandler<GetLastStockTransferForProductQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetLastStockTransferForProductQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetLastStockTransferForProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetLastStockTransferForProductQuery req,
            CancellationToken ct)
        {
            var t = await _svc.GetLastStockTransferForProductAsync(req.ProductId, ct);
            return t is null
                ? null
                : _mapper.Map<LastStockTransferDto>(new { LastTransfer = _mapper.Map<StockTransferDto>(t) });
        }
    }

    // 28. Get last transfer for warehouse
    public class GetLastStockTransferForWarehouseQueryHandler
        : BaseHandler<GetLastStockTransferForWarehouseQuery>
    {
        private readonly IStockTransferQueryService _svc;
        private readonly IMapper _mapper;

        public GetLastStockTransferForWarehouseQueryHandler(
            IStockTransferQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetLastStockTransferForWarehouseQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetLastStockTransferForWarehouseQuery req,
            CancellationToken ct)
        {
            var t = await _svc.GetLastStockTransferForWarehouseAsync(req.WarehouseId, ct);
            return t is null
                ? null
                : _mapper.Map<LastStockTransferDto>(new { LastTransfer = _mapper.Map<StockTransferDto>(t) });
        }
    }
}