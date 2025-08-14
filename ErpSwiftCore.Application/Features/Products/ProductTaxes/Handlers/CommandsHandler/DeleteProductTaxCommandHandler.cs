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

    public class DeleteProductTaxCommandHandler : BaseHandler<DeleteProductTaxCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public DeleteProductTaxCommandHandler(IProductTaxCommandService svc, ILogger<DeleteProductTaxCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(DeleteProductTaxCommand request, CancellationToken ct)
        {
            var ok = await _svc.DeleteTaxAsync(request.TaxId, ct);
            return new { Deleted = ok };
        }
    }

}
