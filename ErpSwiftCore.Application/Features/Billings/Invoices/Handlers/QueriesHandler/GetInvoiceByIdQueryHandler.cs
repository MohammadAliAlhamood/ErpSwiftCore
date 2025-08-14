using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using ErpSwiftCore.Application.Features.Billings.Invoices.Queries; 
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Handlers.QueriesHandler
{
    // 1. Get invoice by ID
    public class GetInvoiceByIdQueryHandler
        : BaseHandler<GetInvoiceByIdQuery>
    {
        private readonly IInvoiceQueryService _svc;
        private readonly IMapper _mapper;

        public GetInvoiceByIdQueryHandler(
            IInvoiceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInvoiceByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoiceByIdQuery req,
            CancellationToken ct)
        {
            var inv = await _svc.GetInvoiceByIdAsync(req.InvoiceId, ct);
            return _mapper.Map<InvoiceDto?>(inv);
        }
    }

    // 2. Get invoices by IDs
    public class GetInvoicesByIdsQueryHandler
        : BaseHandler<GetInvoicesByIdsQuery>
    {
        private readonly IInvoiceQueryService _svc;
        private readonly IMapper _mapper;

        public GetInvoicesByIdsQueryHandler(
            IInvoiceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInvoicesByIdsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoicesByIdsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInvoicesByIdsAsync(req.InvoiceIds, ct);
            return list.Select(i => _mapper.Map<InvoiceDto>(i));
        }
    }

    // 3. Get invoices count
    public class GetInvoicesCountQueryHandler
        : BaseHandler<GetInvoicesCountQuery>
    {
        private readonly IInvoiceQueryService _svc;

        public GetInvoicesCountQueryHandler(
            IInvoiceQueryService svc,
            ILogger<BaseHandler<GetInvoicesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoicesCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetInvoicesCountAsync(req.Status, ct);
            return new { Count = cnt };
        }
    }

    // 4. Get invoice lines
    public class GetInvoiceLinesQueryHandler
        : BaseHandler<GetInvoiceLinesQuery>
    {
        private readonly IInvoiceQueryService _svc;
        private readonly IMapper _mapper;

        public GetInvoiceLinesQueryHandler(
            IInvoiceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInvoiceLinesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoiceLinesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInvoiceLinesAsync(req.InvoiceId, ct);
            return list.Select(l => _mapper.Map<InvoiceLineDto>(l));
        }
    }

    // 5. Get invoice lines count
    public class GetInvoiceLinesCountQueryHandler
        : BaseHandler<GetInvoiceLinesCountQuery>
    {
        private readonly IInvoiceQueryService _svc;

        public GetInvoiceLinesCountQueryHandler(
            IInvoiceQueryService svc,
            ILogger<BaseHandler<GetInvoiceLinesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoiceLinesCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetInvoiceLinesCountAsync(req.InvoiceId, ct);
            return new { Count = cnt };
        }
    }

    // 6. Get invoice approvals
    public class GetInvoiceApprovalsQueryHandler
        : BaseHandler<GetInvoiceApprovalsQuery>
    {
        private readonly IInvoiceQueryService _svc;
        private readonly IMapper _mapper;

        public GetInvoiceApprovalsQueryHandler(
            IInvoiceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInvoiceApprovalsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoiceApprovalsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInvoiceApprovalsAsync(req.InvoiceId, ct);
            return list.Select(a => _mapper.Map<InvoiceApprovalDto>(a));
        }
    }

    // 7. Get invoice approvals count
    public class GetInvoiceApprovalsCountQueryHandler
        : BaseHandler<GetInvoiceApprovalsCountQuery>
    {
        private readonly IInvoiceQueryService _svc;

        public GetInvoiceApprovalsCountQueryHandler(
            IInvoiceQueryService svc,
            ILogger<BaseHandler<GetInvoiceApprovalsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoiceApprovalsCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetInvoiceApprovalsCountAsync(req.InvoiceId, ct);
            return new { Count = cnt };
        }
    }

    // 8. Get invoice approval by ID
    public class GetInvoiceApprovalByIdQueryHandler
        : BaseHandler<GetInvoiceApprovalByIdQuery>
    {
        private readonly IInvoiceQueryService _svc;
        private readonly IMapper _mapper;

        public GetInvoiceApprovalByIdQueryHandler(
            IInvoiceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInvoiceApprovalByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoiceApprovalByIdQuery req,
            CancellationToken ct)
        {
            var apr = await _svc.GetInvoiceApprovalByIdAsync(req.ApprovalId, ct);
            return _mapper.Map<InvoiceApprovalDto?>(apr);
        }
    }

    // 9. Get payments by invoice
    public class GetPaymentsByInvoiceQueryHandler
        : BaseHandler<GetPaymentsByInvoiceQuery>
    {
        private readonly IInvoiceQueryService _svc;
        private readonly IMapper _mapper;

        public GetPaymentsByInvoiceQueryHandler(
            IInvoiceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPaymentsByInvoiceQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPaymentsByInvoiceQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetPaymentsByInvoiceAsync(req.InvoiceId, ct);
            return list.Select(p => _mapper.Map<PaymentDto>(p));
        }
    }

    // 10. Get payments count
    public class GetPaymentsCountQueryHandler
        : BaseHandler<GetPaymentsCountQuery>
    {
        private readonly IInvoiceQueryService _svc;

        public GetPaymentsCountQueryHandler(
            IInvoiceQueryService svc,
            ILogger<BaseHandler<GetPaymentsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPaymentsCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetPaymentsCountAsync(req.InvoiceId, ct);
            return new { Count = cnt };
        }
    }

    // 11. Get payment by ID
    public class GetPaymentByIdQueryHandler
        : BaseHandler<GetPaymentByIdQuery>
    {
        private readonly IInvoiceQueryService _svc;
        private readonly IMapper _mapper;

        public GetPaymentByIdQueryHandler(
            IInvoiceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPaymentByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPaymentByIdQuery req,
            CancellationToken ct)
        {
            var pay = await _svc.GetPaymentByIdAsync(req.PaymentId, ct);
            return _mapper.Map<PaymentDto?>(pay);
        }
    }

   
  
    
   
 

}