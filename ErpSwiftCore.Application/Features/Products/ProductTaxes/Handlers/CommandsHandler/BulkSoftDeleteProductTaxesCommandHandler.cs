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

    public class BulkSoftDeleteProductTaxesCommandHandler : BaseHandler<BulkSoftDeleteProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public BulkSoftDeleteProductTaxesCommandHandler(IProductTaxCommandService svc, ILogger<BulkSoftDeleteProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(BulkSoftDeleteProductTaxesCommand request, CancellationToken ct)
        {
            var count = await _svc.BulkSoftDeleteTaxesAsync(request.TaxIds, ct);
            return new { SoftDeletedCount = count };
        }
    }

}
