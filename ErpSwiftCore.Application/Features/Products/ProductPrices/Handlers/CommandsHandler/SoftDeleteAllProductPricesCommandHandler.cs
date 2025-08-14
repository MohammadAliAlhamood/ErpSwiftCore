using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class SoftDeleteAllProductPricesCommandHandler : BaseHandler<SoftDeleteAllProductPricesCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public SoftDeleteAllProductPricesCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<SoftDeleteAllProductPricesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteAllProductPricesCommand req, CancellationToken ct)
        {
            var count = await _svc.SoftDeleteAllPricesAsync(ct);
            return new { SoftDeletedCount = count };
        }
    }


}
