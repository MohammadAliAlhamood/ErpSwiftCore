using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
using ErpSwiftCore.Application.Features.Billings.Payments.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Billings.Payments.Handlers.QueriesHandler
{
    #region Query Handlers

    // 1. Get payments by invoice
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

    // 2. Get invoice approvals
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

    // 3. Get invoice lines
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

    // 4. Get invoice by ID
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

    // 5. Get invoice approval by ID
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
            var a = await _svc.GetInvoiceApprovalByIdAsync(req.ApprovalId, ct);
            return _mapper.Map<InvoiceApprovalDto?>(a);
        }
    }

    // 6. Get payment by ID
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
            var p = await _svc.GetPaymentByIdAsync(req.PaymentId, ct);
            return _mapper.Map<PaymentDto?>(p);
        }
    }

    // 7. Get invoices by IDs
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

    // 8. Get payments count
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
            var count = await _svc.GetPaymentsCountAsync(req.InvoiceId, ct);
            return new CountResultDto { Count = count };
        }
    }

    // 9. Get approvals count
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
            var count = await _svc.GetInvoiceApprovalsCountAsync(req.InvoiceId, ct);
            return new CountResultDto { Count = count };
        }
    }

    // 10. Get invoices count
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
            var count = await _svc.GetInvoicesCountAsync(req.Status, ct);
            return new CountResultDto { Count = count };
        }
    }

    // 11. Get invoice lines count
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
            var count = await _svc.GetInvoiceLinesCountAsync(req.InvoiceId, ct);
            return new CountResultDto { Count = count };
        }
    }

    // 12. Validate invoice
    public class ValidateInvoiceQueryHandler
        : BaseHandler<ValidateInvoiceQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public ValidateInvoiceQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<ValidateInvoiceQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateInvoiceQuery req,
            CancellationToken ct)
        {
            var ok = await _validation.ValidateInvoiceAsync(req.InvoiceId, ct);
            return new ExistsResultDto { Exists = ok };
        }
    }

    // 13. Check invoice exists
    public class CheckInvoiceExistsQueryHandler
        : BaseHandler<CheckInvoiceExistsQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public CheckInvoiceExistsQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<CheckInvoiceExistsQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckInvoiceExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _validation.InvoiceExistsAsync(req.InvoiceId, ct);
            return new ExistsResultDto { Exists = ok };
        }
    }

    // 14. Check invoice line exists
    public class CheckInvoiceLineExistsQueryHandler
        : BaseHandler<CheckInvoiceLineExistsQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public CheckInvoiceLineExistsQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<CheckInvoiceLineExistsQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckInvoiceLineExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _validation.InvoiceLineExistsAsync(req.LineId, ct);
            return new ExistsResultDto { Exists = ok };
        }
    }

    // 15. Check invoice approval exists
    public class CheckInvoiceApprovalExistsQueryHandler
        : BaseHandler<CheckInvoiceApprovalExistsQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public CheckInvoiceApprovalExistsQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<CheckInvoiceApprovalExistsQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckInvoiceApprovalExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _validation.InvoiceApprovalExistsAsync(req.ApprovalId, ct);
            return new ExistsResultDto { Exists = ok };
        }
    }

    // 16. Check payment exists
    public class CheckPaymentExistsQueryHandler
        : BaseHandler<CheckPaymentExistsQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public CheckPaymentExistsQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<CheckPaymentExistsQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckPaymentExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _validation.PaymentExistsAsync(req.PaymentId, ct);
            return new ExistsResultDto { Exists = ok };
        }
    }

    // 17. Check invoice has any lines
    public class CheckHasInvoiceLinesQueryHandler
        : BaseHandler<CheckHasInvoiceLinesQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public CheckHasInvoiceLinesQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<CheckHasInvoiceLinesQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckHasInvoiceLinesQuery req,
            CancellationToken ct)
        {
            var ok = await _validation.HasInvoiceLinesAsync(req.InvoiceId, ct);
            return new ExistsResultDto { Exists = ok };
        }
    }

    // 18. Check invoice linked to currency
    public class CheckInvoiceLinkedToCurrencyQueryHandler
        : BaseHandler<CheckInvoiceLinkedToCurrencyQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public CheckInvoiceLinkedToCurrencyQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<CheckInvoiceLinkedToCurrencyQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckInvoiceLinkedToCurrencyQuery req,
            CancellationToken ct)
        {
            var ok = await _validation.IsInvoiceLinkedToCurrencyAsync(req.InvoiceId, req.CurrencyId, ct);
            return new ExistsResultDto { Exists = ok };
        }
    }

    // 19. Calculate invoice total
    public class CalculateInvoiceTotalQueryHandler
        : BaseHandler<CalculateInvoiceTotalQuery>
    {
        private readonly IInvoiceValidationService _validation;

        public CalculateInvoiceTotalQueryHandler(
            IInvoiceValidationService validation,
            ILogger<BaseHandler<CalculateInvoiceTotalQuery>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CalculateInvoiceTotalQuery req,
            CancellationToken ct)
        {
            var total = await _validation.CalculateInvoiceTotalAsync(req.InvoiceId, ct);
            return new DecimalResultDto { Value = total };
        }
    }

    #endregion
}
