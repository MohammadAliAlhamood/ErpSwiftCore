using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class RestoreProductPriceCommandHandler : BaseHandler<RestoreProductPriceCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public RestoreProductPriceCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<RestoreProductPriceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreProductPriceCommand req, CancellationToken ct)
        {
            await _svc.RestorePriceAsync(req.PriceId, ct);
            return new { RestoredId = req.PriceId };
        }
    }




}
