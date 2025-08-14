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
    public class SoftDeleteProductTaxesRangeCommandHandler : BaseHandler<SoftDeleteProductTaxesRangeCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public SoftDeleteProductTaxesRangeCommandHandler(IProductTaxCommandService svc, ILogger<SoftDeleteProductTaxesRangeCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductTaxesRangeCommand request, CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteTaxesRangeAsync(request.TaxIds, ct);
            return new { SoftDeleted = ok };
        }
    }


}
