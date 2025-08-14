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

    public class SoftDeleteProductTaxCommandHandler : BaseHandler<SoftDeleteProductTaxCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public SoftDeleteProductTaxCommandHandler(IProductTaxCommandService svc, ILogger<SoftDeleteProductTaxCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductTaxCommand request, CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteTaxAsync(request.TaxId, ct);
            return new { SoftDeleted = ok };
        }
    }

}
