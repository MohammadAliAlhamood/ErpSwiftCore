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

    public class BulkDeleteProductTaxesCommandHandler : BaseHandler<BulkDeleteProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public BulkDeleteProductTaxesCommandHandler(IProductTaxCommandService svc, ILogger<BulkDeleteProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(BulkDeleteProductTaxesCommand request, CancellationToken ct)
        {
            var count = await _svc.BulkDeleteTaxesAsync(request.TaxIds, ct);
            return new { DeletedCount = count };
        }
    }


}
