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

    public class SoftDeleteAllProductTaxesCommandHandler : BaseHandler<SoftDeleteAllProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public SoftDeleteAllProductTaxesCommandHandler(IProductTaxCommandService svc, ILogger<SoftDeleteAllProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteAllProductTaxesCommand request, CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteAllTaxesAsync(ct);
            return new { SoftDeletedAll = ok };
        }
    }


}
