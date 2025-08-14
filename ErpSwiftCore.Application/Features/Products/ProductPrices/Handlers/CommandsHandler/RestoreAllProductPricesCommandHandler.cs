using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class RestoreAllProductPricesCommandHandler : BaseHandler<RestoreAllProductPricesCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public RestoreAllProductPricesCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<RestoreAllProductPricesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreAllProductPricesCommand req, CancellationToken ct)
        {
            var count = await _svc.RestoreAllPricesAsync(ct);
            return new { RestoredCount = count };
        }
    }



}
