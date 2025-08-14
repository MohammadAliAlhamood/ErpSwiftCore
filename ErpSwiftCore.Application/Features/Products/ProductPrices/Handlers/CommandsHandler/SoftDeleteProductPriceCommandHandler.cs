using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class SoftDeleteProductPriceCommandHandler : BaseHandler<SoftDeleteProductPriceCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public SoftDeleteProductPriceCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<SoftDeleteProductPriceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductPriceCommand req, CancellationToken ct)
        {
            await _svc.SoftDeletePriceAsync(req.PriceId, ct);
            return new { SoftDeletedId = req.PriceId };
        }
    }


}
