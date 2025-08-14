using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Queries;
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Handlers.QueriesHandler
{

    // 1. Get taxes by invoice
    public class GetTaxesByInvoiceQueryHandler
        : BaseHandler<GetTaxesByInvoiceQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;
        private readonly IMapper _mapper;

        public GetTaxesByInvoiceQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTaxesByInvoiceQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTaxesByInvoiceQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetTaxesByInvoiceAsync(req.InvoiceId, ct);
            return list.Select(t => _mapper.Map<InvoiceTaxDto>(t));
        }
    }

    // 2. Get taxes count
    public class GetTaxesCountQueryHandler
        : BaseHandler<GetTaxesCountQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;

        public GetTaxesCountQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            ILogger<BaseHandler<GetTaxesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTaxesCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetTaxesCountAsync(req.InvoiceId, ct);
            return new { Count = cnt };
        }
    }

    // 3. Get tax by ID
    public class GetTaxByIdQueryHandler
        : BaseHandler<GetTaxByIdQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;
        private readonly IMapper _mapper;

        public GetTaxByIdQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTaxByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTaxByIdQuery req,
            CancellationToken ct)
        {
            var tax = await _svc.GetTaxByIdAsync(req.TaxId, ct);
            return _mapper.Map<InvoiceTaxDto?>(tax);
        }
    }

    // 4. Get discounts by invoice
    public class GetDiscountsByInvoiceQueryHandler
        : BaseHandler<GetDiscountsByInvoiceQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;
        private readonly IMapper _mapper;

        public GetDiscountsByInvoiceQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetDiscountsByInvoiceQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetDiscountsByInvoiceQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetDiscountsByInvoiceAsync(req.InvoiceId, ct);
            return list.Select(d => _mapper.Map<InvoiceDiscountDto>(d));
        }
    }

    // 5. Get discounts count
    public class GetDiscountsCountQueryHandler
        : BaseHandler<GetDiscountsCountQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;

        public GetDiscountsCountQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            ILogger<BaseHandler<GetDiscountsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetDiscountsCountQuery req,
            CancellationToken ct)
        {
            var cnt = await _svc.GetDiscountsCountAsync(req.InvoiceId, ct);
            return new { Count = cnt };
        }
    }

    // 6. Get discount by ID
    public class GetDiscountByIdQueryHandler
        : BaseHandler<GetDiscountByIdQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;
        private readonly IMapper _mapper;

        public GetDiscountByIdQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetDiscountByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetDiscountByIdQuery req,
            CancellationToken ct)
        {
            var disc = await _svc.GetDiscountByIdAsync(req.DiscountId, ct);
            return _mapper.Map<InvoiceDiscountDto?>(disc);
        }
    }

    // 7. Get total tax amount
    public class GetTotalTaxAmountQueryHandler
        : BaseHandler<GetTotalTaxAmountQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;

        public GetTotalTaxAmountQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            ILogger<BaseHandler<GetTotalTaxAmountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalTaxAmountQuery req,
            CancellationToken ct)
        {
            var total = await _svc.GetTotalTaxAmountAsync(req.InvoiceId, ct);
            return new { TotalTax = total };
        }
    }

    // 8. Get total discount amount
    public class GetTotalDiscountAmountQueryHandler
        : BaseHandler<GetTotalDiscountAmountQuery>
    {
        private readonly IInvoiceTaxDiscountQueryService _svc;

        public GetTotalDiscountAmountQueryHandler(
            IInvoiceTaxDiscountQueryService svc,
            ILogger<BaseHandler<GetTotalDiscountAmountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetTotalDiscountAmountQuery req,
            CancellationToken ct)
        {
            var total = await _svc.GetTotalDiscountAmountAsync(req.InvoiceId, ct);
            return new { TotalDiscount = total };
        }
    }

    // 9. Validate tax & discount
    public class ValidateTaxAndDiscountQueryHandler
        : BaseHandler<ValidateTaxAndDiscountQuery>
    {
        private readonly IInvoiceTaxDiscountValidationService _svc;

        public ValidateTaxAndDiscountQueryHandler(
            IInvoiceTaxDiscountValidationService svc,
            ILogger<BaseHandler<ValidateTaxAndDiscountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateTaxAndDiscountQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.ValidateTaxAndDiscountAsync(req.InvoiceId, ct);
            return new { Valid = ok };
        }
    }

    // 10. Tax exists
    public class TaxExistsQueryHandler
        : BaseHandler<TaxExistsQuery>
    {
        private readonly IInvoiceTaxDiscountValidationService _svc;

        public TaxExistsQueryHandler(
            IInvoiceTaxDiscountValidationService svc,
            ILogger<BaseHandler<TaxExistsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            TaxExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.TaxExistsAsync(req.TaxId, ct);
            return new { Exists = ok };
        }
    }

    // 11. Discount exists
    public class DiscountExistsQueryHandler
        : BaseHandler<DiscountExistsQuery>
    {
        private readonly IInvoiceTaxDiscountValidationService _svc;

        public DiscountExistsQueryHandler(
            IInvoiceTaxDiscountValidationService svc,
            ILogger<BaseHandler<DiscountExistsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DiscountExistsQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.DiscountExistsAsync(req.DiscountId, ct);
            return new { Exists = ok };
        }
    }

    // 12. Has taxes
    public class HasTaxesQueryHandler
        : BaseHandler<HasTaxesQuery>
    {
        private readonly IInvoiceTaxDiscountValidationService _svc;

        public HasTaxesQueryHandler(
            IInvoiceTaxDiscountValidationService svc,
            ILogger<BaseHandler<HasTaxesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            HasTaxesQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.HasTaxesAsync(req.InvoiceId, ct);
            return new { Has = ok };
        }
    }

    // 13. Has discounts
    public class HasDiscountsQueryHandler
        : BaseHandler<HasDiscountsQuery>
    {
        private readonly IInvoiceTaxDiscountValidationService _svc;

        public HasDiscountsQueryHandler(
            IInvoiceTaxDiscountValidationService svc,
            ILogger<BaseHandler<HasDiscountsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            HasDiscountsQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.HasDiscountsAsync(req.InvoiceId, ct);
            return new { Has = ok };
        }
    }

    // 14. Is linked to currency
    public class IsInvoiceLinkedToCurrencyQueryHandler
        : BaseHandler<IsInvoiceLinkedToCurrencyQuery>
    {
        private readonly IInvoiceTaxDiscountValidationService _svc;

        public IsInvoiceLinkedToCurrencyQueryHandler(
            IInvoiceTaxDiscountValidationService svc,
            ILogger<BaseHandler<IsInvoiceLinkedToCurrencyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            IsInvoiceLinkedToCurrencyQuery req,
            CancellationToken ct)
        {
            var ok = await _svc.IsInvoiceLinkedToCurrencyAsync(req.InvoiceId, req.CurrencyId, ct);
            return new { Linked = ok };
        }
    }
}