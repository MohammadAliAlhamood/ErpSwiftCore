using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Commands;
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Handlers.CommandsHandler
{

    // 1. Create taxes and discounts
    public class CreateTaxesAndDiscountsCommandHandler
        : BaseHandler<CreateTaxesAndDiscountsCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public CreateTaxesAndDiscountsCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<CreateTaxesAndDiscountsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            CreateTaxesAndDiscountsCommand req,
            CancellationToken ct)
        {
            var (taxes, discounts) = await _svc.CreateTaxesAndDiscountsAsync(
                req.Dto.InvoiceId,
                req.Dto.Taxes.Select(d => _mapper.Map<InvoiceTax>(d)),
                req.Dto.Discounts.Select(d => _mapper.Map<InvoiceDiscount>(d)),
                ct
            );

            return new
            {
                Taxes = taxes.Select(t => _mapper.Map<InvoiceTaxDto>(t)),
                Discounts = discounts.Select(d => _mapper.Map<InvoiceDiscountDto>(d))
            };
        }
    }

    // 2. Update taxes and discounts
    public class UpdateTaxesAndDiscountsCommandHandler
        : BaseHandler<UpdateTaxesAndDiscountsCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateTaxesAndDiscountsCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateTaxesAndDiscountsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateTaxesAndDiscountsCommand req,
            CancellationToken ct)
        {
            var (updatedTaxes, updatedDiscounts) = await _svc.UpdateTaxesAndDiscountsAsync(
                req.Dto.InvoiceId,
                req.Dto.TaxesToAdd?.Select(d => _mapper.Map<InvoiceTax>(d)),
                req.Dto.TaxesToUpdate?.Select(d => _mapper.Map<InvoiceTax>(d)),
                req.Dto.TaxIdsToDelete,
                req.Dto.DiscountsToAdd?.Select(d => _mapper.Map<InvoiceDiscount>(d)),
                req.Dto.DiscountsToUpdate?.Select(d => _mapper.Map<InvoiceDiscount>(d)),
                req.Dto.DiscountIdsToDelete,
                ct
            );

            return new
            {
                UpdatedTaxes = updatedTaxes.Select(t => _mapper.Map<InvoiceTaxDto>(t)),
                UpdatedDiscounts = updatedDiscounts.Select(d => _mapper.Map<InvoiceDiscountDto>(d))
            };
        }
    }

    // 3. Add single tax
    public class AddInvoiceTaxCommandHandler
        : BaseHandler<AddInvoiceTaxCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public AddInvoiceTaxCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<AddInvoiceTaxCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddInvoiceTaxCommand req,
            CancellationToken ct)
        {
            var tax = _mapper.Map<InvoiceTax>(req.Dto);
            var added = await _svc.AddTaxAsync(req.InvoiceId, tax, ct);
            return _mapper.Map<InvoiceTaxDto>(added);
        }
    }

    // 4. Add multiple taxes
    public class AddInvoiceTaxesCommandHandler
        : BaseHandler<AddInvoiceTaxesCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public AddInvoiceTaxesCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<AddInvoiceTaxesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddInvoiceTaxesCommand req,
            CancellationToken ct)
        {
            var taxes = req.Dtos.Select(d => _mapper.Map<InvoiceTax>(d));
            var added = await _svc.AddTaxesAsync(req.InvoiceId, taxes, ct);
            return added.Select(t => _mapper.Map<InvoiceTaxDto>(t));
        }
    }

    // 5. Update tax
    public class UpdateInvoiceTaxCommandHandler
        : BaseHandler<UpdateInvoiceTaxCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateInvoiceTaxCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateInvoiceTaxCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateInvoiceTaxCommand req,
            CancellationToken ct)
        {
            var tax = _mapper.Map<InvoiceTax>(req.Dto);
            var updated = await _svc.UpdateTaxAsync(tax, ct);
            return _mapper.Map<InvoiceTaxDto>(updated);
        }
    }

    // 6. Delete tax
    public class DeleteInvoiceTaxCommandHandler
        : BaseHandler<DeleteInvoiceTaxCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;

        public DeleteInvoiceTaxCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            ILogger<BaseHandler<DeleteInvoiceTaxCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteInvoiceTaxCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteTaxAsync(req.TaxId, ct);
            return new { Success = ok };
        }
    }

    // 7. Delete all taxes
    public class DeleteAllTaxesOfInvoiceCommandHandler
        : BaseHandler<DeleteAllTaxesOfInvoiceCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;

        public DeleteAllTaxesOfInvoiceCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            ILogger<BaseHandler<DeleteAllTaxesOfInvoiceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllTaxesOfInvoiceCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllTaxesOfInvoiceAsync(req.InvoiceId, ct);
            return new { Success = ok };
        }
    }

    // 8. Add single discount
    public class AddInvoiceDiscountCommandHandler
        : BaseHandler<AddInvoiceDiscountCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public AddInvoiceDiscountCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<AddInvoiceDiscountCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddInvoiceDiscountCommand req,
            CancellationToken ct)
        {
            var disc = _mapper.Map<InvoiceDiscount>(req.Dto);
            var added = await _svc.AddDiscountAsync(req.InvoiceId, disc, ct);
            return _mapper.Map<InvoiceDiscountDto>(added);
        }
    }

    // 9. Add multiple discounts
    public class AddInvoiceDiscountsCommandHandler
        : BaseHandler<AddInvoiceDiscountsCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public AddInvoiceDiscountsCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<AddInvoiceDiscountsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddInvoiceDiscountsCommand req,
            CancellationToken ct)
        {
            var discs = req.Dtos.Select(d => _mapper.Map<InvoiceDiscount>(d));
            var added = await _svc.AddDiscountsAsync(req.InvoiceId, discs, ct);
            return added.Select(d => _mapper.Map<InvoiceDiscountDto>(d));
        }
    }

    // 10. Update discount
    public class UpdateInvoiceDiscountCommandHandler
        : BaseHandler<UpdateInvoiceDiscountCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateInvoiceDiscountCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateInvoiceDiscountCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateInvoiceDiscountCommand req,
            CancellationToken ct)
        {
            var disc = _mapper.Map<InvoiceDiscount>(req.Dto);
            var updated = await _svc.UpdateDiscountAsync(disc, ct);
            return _mapper.Map<InvoiceDiscountDto>(updated);
        }
    }

    // 11. Delete discount
    public class DeleteInvoiceDiscountCommandHandler
        : BaseHandler<DeleteInvoiceDiscountCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;

        public DeleteInvoiceDiscountCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            ILogger<BaseHandler<DeleteInvoiceDiscountCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteInvoiceDiscountCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteDiscountAsync(req.DiscountId, ct);
            return new { Success = ok };
        }
    }

    // 12. Delete all discounts
    public class DeleteAllDiscountsOfInvoiceCommandHandler
        : BaseHandler<DeleteAllDiscountsOfInvoiceCommand>
    {
        private readonly IInvoiceTaxDiscountCommandService _svc;

        public DeleteAllDiscountsOfInvoiceCommandHandler(
            IInvoiceTaxDiscountCommandService svc,
            ILogger<BaseHandler<DeleteAllDiscountsOfInvoiceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllDiscountsOfInvoiceCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllDiscountsOfInvoiceAsync(req.InvoiceId, ct);
            return new { Success = ok };
        }
    }
}