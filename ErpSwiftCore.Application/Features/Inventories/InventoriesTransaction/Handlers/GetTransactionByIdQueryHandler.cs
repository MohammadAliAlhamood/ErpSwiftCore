using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Queries;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryTransactionService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Handlers
{
    // 1. Get single transaction by its ID
    public class GetTransactionByIdQueryHandler
        : BaseHandler<GetTransactionByIdQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTransactionByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionByIdQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetTransactionByIdAsync(req.TransactionId, ct);
            return entity is null
                ? null
                : _mapper.Map<InventoryTransactionDto>(entity);
        }
    }

    // 2. Get first transaction for a given inventory
    public class GetFirstTransactionForInventoryQueryHandler
        : BaseHandler<GetFirstTransactionForInventoryQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetFirstTransactionForInventoryQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetFirstTransactionForInventoryQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetFirstTransactionForInventoryQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetFirstTransactionForInventoryAsync(req.InventoryId, ct);
            return entity is null
                ? null
                : _mapper.Map<InventoryTransactionDto>(entity);
        }
    }

    // 3. Get last transaction for a given inventory
    public class GetLastTransactionForInventoryQueryHandler
        : BaseHandler<GetLastTransactionForInventoryQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetLastTransactionForInventoryQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetLastTransactionForInventoryQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetLastTransactionForInventoryQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetLastTransactionForInventoryAsync(req.InventoryId, ct);
            return entity is null
                ? null
                : _mapper.Map<InventoryTransactionDto>(entity);
        }
    }

    // 4. Get all transactions for a given inventory
    public class GetTransactionsByInventoryIdQueryHandler
        : BaseHandler<GetTransactionsByInventoryIdQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetTransactionsByInventoryIdQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTransactionsByInventoryIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionsByInventoryIdQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTransactionsByInventoryIdAsync(req.InventoryId, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 5. Get all transactions for a given product
    public class GetTransactionsByProductIdQueryHandler
        : BaseHandler<GetTransactionsByProductIdQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetTransactionsByProductIdQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTransactionsByProductIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionsByProductIdQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTransactionsByProductIdAsync(req.ProductId, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 6. Get all transactions for a given warehouse
    public class GetTransactionsByWarehouseIdQueryHandler
        : BaseHandler<GetTransactionsByWarehouseIdQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetTransactionsByWarehouseIdQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTransactionsByWarehouseIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionsByWarehouseIdQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTransactionsByWarehouseIdAsync(req.WarehouseId, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 7. Get all transactions of a specific type
    public class GetTransactionsByTypeQueryHandler
        : BaseHandler<GetTransactionsByTypeQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetTransactionsByTypeQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTransactionsByTypeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionsByTypeQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTransactionsByTypeAsync(req.TransactionType, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 8. Get all transactions in a date range
    public class GetTransactionsByDateRangeQueryHandler
        : BaseHandler<GetTransactionsByDateRangeQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetTransactionsByDateRangeQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTransactionsByDateRangeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionsByDateRangeQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTransactionsByDateRangeAsync(req.From, req.To, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 9. Get all transactions affecting the available balance for an inventory
    public class GetTransactionsAffectingBalanceQueryHandler
        : BaseHandler<GetTransactionsAffectingBalanceQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public GetTransactionsAffectingBalanceQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTransactionsAffectingBalanceQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionsAffectingBalanceQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTransactionsAffectingBalanceAsync(req.InventoryId, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 10. Search transactions whose notes contain a given term
    public class SearchTransactionsByNotesQueryHandler
        : BaseHandler<SearchTransactionsByNotesQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;
        private readonly IMapper _mapper;

        public SearchTransactionsByNotesQueryHandler(
            IInventoryTransactionQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<SearchTransactionsByNotesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            SearchTransactionsByNotesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTransactionsWithNotesContainingAsync(req.NoteTerm, ct);
            return list.Select(t => _mapper.Map<InventoryTransactionDto>(t));
        }
    }

    // 11. Get total count of all transactions
    public class GetTransactionsCountQueryHandler
        : BaseHandler<GetTransactionsCountQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;

        public GetTransactionsCountQueryHandler(
            IInventoryTransactionQueryService svc,
            ILogger<BaseHandler<GetTransactionsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTransactionsCountQuery req,
            CancellationToken ct)
        {
            var count = await _svc.GetTransactionsCountAsync(ct);
            return count;
        }
    }

    // 12. Sum quantity moved for a product in a date range
    public class SumQuantityByProductAndDateRangeQueryHandler
        : BaseHandler<SumQuantityByProductAndDateRangeQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;

        public SumQuantityByProductAndDateRangeQueryHandler(
            IInventoryTransactionQueryService svc,
            ILogger<BaseHandler<SumQuantityByProductAndDateRangeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            SumQuantityByProductAndDateRangeQuery req,
            CancellationToken ct)
        {
            var total = await _svc.SumQuantityByProductAndDateRangeAsync(req.ProductId, req.From, req.To, ct);
            return new InventoryTransactionAggregateDto
            {
                ProductID = req.ProductId,
                TotalQuantity = total,
                TurnoverRate = 0m
            };
        }
    }

    // 13. Calculate turnover rate for a product in a date range
    public class GetTurnoverRateQueryHandler
        : BaseHandler<GetTurnoverRateQuery>
    {
        private readonly IInventoryTransactionQueryService _svc;

        public GetTurnoverRateQueryHandler(
            IInventoryTransactionQueryService svc,
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
            return new InventoryTransactionAggregateDto
            {
                ProductID = req.ProductId,
                TotalQuantity = 0,
                TurnoverRate = rate
            };
        }
    }
}







