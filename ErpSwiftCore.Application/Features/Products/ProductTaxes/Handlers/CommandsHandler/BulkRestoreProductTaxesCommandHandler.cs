using ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Handlers.CommandsHandler
{
    public class BulkRestoreProductTaxesCommandHandler : BaseHandler<BulkRestoreProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public BulkRestoreProductTaxesCommandHandler(IProductTaxCommandService svc, ILogger<BulkRestoreProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(BulkRestoreProductTaxesCommand request, CancellationToken ct)
        {
            var count = await _svc.BulkRestoreTaxesAsync(request.TaxIds, ct);
            return new { RestoredCount = count };
        }
    }

}
