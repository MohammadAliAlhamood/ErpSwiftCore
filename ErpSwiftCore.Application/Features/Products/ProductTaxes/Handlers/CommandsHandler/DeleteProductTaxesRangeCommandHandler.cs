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
    public class DeleteProductTaxesRangeCommandHandler : BaseHandler<DeleteProductTaxesRangeCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public DeleteProductTaxesRangeCommandHandler(IProductTaxCommandService svc, ILogger<DeleteProductTaxesRangeCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(DeleteProductTaxesRangeCommand request, CancellationToken ct)
        {
            var ok = await _svc.DeleteTaxesRangeAsync(request.TaxIds, ct);
            return new { Deleted = ok };
        }
    }


}
