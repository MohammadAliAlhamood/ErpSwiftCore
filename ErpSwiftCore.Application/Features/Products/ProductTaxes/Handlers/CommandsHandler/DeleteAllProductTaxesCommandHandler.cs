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
    public class DeleteAllProductTaxesCommandHandler : BaseHandler<DeleteAllProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        public DeleteAllProductTaxesCommandHandler(IProductTaxCommandService svc, ILogger<DeleteAllProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(DeleteAllProductTaxesCommand request, CancellationToken ct)
        {
            var ok = await _svc.DeleteAllTaxesAsync(ct);
            return new { DeletedAll = ok };
        }
    }


}
