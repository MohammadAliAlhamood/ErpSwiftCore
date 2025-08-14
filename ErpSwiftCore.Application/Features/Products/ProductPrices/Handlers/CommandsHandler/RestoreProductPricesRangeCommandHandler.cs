using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class RestoreProductPricesRangeCommandHandler : BaseHandler<RestoreProductPricesRangeCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public RestoreProductPricesRangeCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<RestoreProductPricesRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreProductPricesRangeCommand req, CancellationToken ct)
        {
            var count = await _svc.RestorePricesRangeAsync(req.PriceIds, ct);
            return new { RestoredCount = count };
        }
    }


}
