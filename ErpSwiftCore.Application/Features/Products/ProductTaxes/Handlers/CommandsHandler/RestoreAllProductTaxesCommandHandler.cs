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

    public class RestoreAllProductTaxesCommandHandler : BaseHandler<RestoreAllProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public RestoreAllProductTaxesCommandHandler(IProductTaxCommandService svc, ILogger<RestoreAllProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(RestoreAllProductTaxesCommand request, CancellationToken ct)
        {
            var ok = await _svc.RestoreAllTaxesAsync(ct);
            return new { RestoredAll = ok };
        }
    }

}
