using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class SoftDeleteProductPricesRangeCommandHandler : BaseHandler<SoftDeleteProductPricesRangeCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public SoftDeleteProductPricesRangeCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<SoftDeleteProductPricesRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductPricesRangeCommand req, CancellationToken ct)
        {
            var count = await _svc.SoftDeletePricesRangeAsync(req.PriceIds, ct);
            return new { SoftDeletedCount = count };
        }
    }


}
