using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.Invoices.Commands;
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using ErpSwiftCore.Application.Features.Billings.Payments.Commands;
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Handlers.CommandsHandler
{
     
    public class DeleteInvoiceCommandHandler
        : BaseHandler<DeleteInvoiceCommand>
    {
        private readonly IInvoiceCommandService _svc;

        public DeleteInvoiceCommandHandler(
            IInvoiceCommandService svc,
            ILogger<BaseHandler<DeleteInvoiceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteInvoiceCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteInvoiceAsync(req.InvoiceId, ct);
            return new { Success = ok };
        }
    }

    

 

    // 7. Delete invoice line
    public class DeleteInvoiceLineCommandHandler
        : BaseHandler<DeleteInvoiceLineCommand>
    {
        private readonly IInvoiceCommandService _svc;

        public DeleteInvoiceLineCommandHandler(
            IInvoiceCommandService svc,
            ILogger<BaseHandler<DeleteInvoiceLineCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(  DeleteInvoiceLineCommand req,    CancellationToken ct)
        { 
            return await _svc.DeleteInvoiceLineAsync(req.LineId, ct);
        }
    }

    // 8. Delete all lines of invoice
    public class DeleteAllLinesOfInvoiceCommandHandler
        : BaseHandler<DeleteAllLinesOfInvoiceCommand>
    {
        private readonly IInvoiceCommandService _svc;

        public DeleteAllLinesOfInvoiceCommandHandler(
            IInvoiceCommandService svc,
            ILogger<BaseHandler<DeleteAllLinesOfInvoiceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllLinesOfInvoiceCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllLinesOfInvoiceAsync(req.InvoiceId, ct);
            return new { Success = ok };
        }
    }

    // 9. Add invoice approval
    public class AddInvoiceApprovalCommandHandler
        : BaseHandler<AddInvoiceApprovalCommand>
    {
        private readonly IInvoiceCommandService _svc;
        private readonly IMapper _mapper;

        public AddInvoiceApprovalCommandHandler(
            IInvoiceCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<AddInvoiceApprovalCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddInvoiceApprovalCommand req,
            CancellationToken ct)
        {
            var apr = _mapper.Map<InvoiceApproval>(req.Dto);
            var added = await _svc.AddInvoiceApprovalAsync(req.InvoiceId, apr, ct);
            return _mapper.Map<InvoiceApprovalDto>(added);
        }
    }

    // 10. Update invoice approval
    public class UpdateInvoiceApprovalCommandHandler
        : BaseHandler<UpdateInvoiceApprovalCommand>
    {
        private readonly IInvoiceCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateInvoiceApprovalCommandHandler(
            IInvoiceCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateInvoiceApprovalCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateInvoiceApprovalCommand req,
            CancellationToken ct)
        {
            var apr = _mapper.Map<InvoiceApproval>(req.Dto);
            var updated = await _svc.UpdateInvoiceApprovalAsync(apr, ct);
            return _mapper.Map<InvoiceApprovalDto>(updated);
        }
    }

    // 11. Delete invoice approval
    public class DeleteInvoiceApprovalCommandHandler
        : BaseHandler<DeleteInvoiceApprovalCommand>
    {
        private readonly IInvoiceCommandService _svc;

        public DeleteInvoiceApprovalCommandHandler(
            IInvoiceCommandService svc,
            ILogger<BaseHandler<DeleteInvoiceApprovalCommand>> logger  ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteInvoiceApprovalCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteInvoiceApprovalAsync(req.ApprovalId, ct);
            return new { Success = ok };
        }
    }

    // 12. Add payment
    public class AddPaymentCommandHandler
        : BaseHandler<AddPaymentCommand>
    {
        private readonly IInvoiceCommandService _svc;
        private readonly IMapper _mapper;

        public AddPaymentCommandHandler(
            IInvoiceCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<AddPaymentCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddPaymentCommand req,
            CancellationToken ct)
        {
            var pay = _mapper.Map<Payment>(req.Dto);
            var added = await _svc.AddPaymentAsync(req.Dto.InvoiceId, pay, ct);
            return _mapper.Map<PaymentDto>(added);
        }
    }

   

    // 14. Delete payment
    public class DeletePaymentCommandHandler
        : BaseHandler<DeletePaymentCommand>
    {
        private readonly IInvoiceCommandService _svc;

        public DeletePaymentCommandHandler(
            IInvoiceCommandService svc,
            ILogger<BaseHandler<DeletePaymentCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeletePaymentCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeletePaymentAsync(req.PaymentId, ct);
            return new { Success = ok };
        }
    }

    // 15. Change invoice status
    public class ChangeInvoiceStatusCommandHandler
        : BaseHandler<ChangeInvoiceStatusCommand>
    {
        private readonly IInvoiceCommandService _svc;

        public ChangeInvoiceStatusCommandHandler(
            IInvoiceCommandService svc,
            ILogger<BaseHandler<ChangeInvoiceStatusCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            ChangeInvoiceStatusCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.ChangeInvoiceStatusAsync(
                req.Dto.InvoiceId, req.Dto.NewStatus, ct
            );
            return new { Success = ok };
        }
    }

    // 16. Bulk delete invoices
    public class BulkDeleteInvoicesCommandHandler
        : BaseHandler<BulkDeleteInvoicesCommand>
    {
        private readonly IInvoiceCommandService _svc;

        public BulkDeleteInvoicesCommandHandler(
            IInvoiceCommandService svc,
            ILogger<BaseHandler<BulkDeleteInvoicesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkDeleteInvoicesCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteInvoicesRangeAsync(req.InvoiceIds, ct);
            return new { Success = ok };
        }
    }
}