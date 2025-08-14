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

    public class RestoreProductTaxCommandHandler : BaseHandler<RestoreProductTaxCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public RestoreProductTaxCommandHandler(IProductTaxCommandService svc, ILogger<RestoreProductTaxCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(RestoreProductTaxCommand request, CancellationToken ct)
        {
            var ok = await _svc.RestoreTaxAsync(request.TaxId, ct);
            return new { Restored = ok };
        }
    }
}
