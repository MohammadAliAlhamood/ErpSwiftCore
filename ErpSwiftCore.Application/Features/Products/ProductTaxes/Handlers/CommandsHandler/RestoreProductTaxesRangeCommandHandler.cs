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

    public class RestoreProductTaxesRangeCommandHandler : BaseHandler<RestoreProductTaxesRangeCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public RestoreProductTaxesRangeCommandHandler(IProductTaxCommandService svc, ILogger<RestoreProductTaxesRangeCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(RestoreProductTaxesRangeCommand request, CancellationToken ct)
        {
            var ok = await _svc.RestoreTaxesRangeAsync(request.TaxIds, ct);
            return new { Restored = ok };
        }
    }


}
