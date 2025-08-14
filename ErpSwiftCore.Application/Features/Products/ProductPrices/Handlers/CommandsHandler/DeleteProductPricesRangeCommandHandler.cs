using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class DeleteProductPricesRangeCommandHandler : BaseHandler<DeleteProductPricesRangeCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public DeleteProductPricesRangeCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<DeleteProductPricesRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteProductPricesRangeCommand req, CancellationToken ct)
        {
            var count = await _svc.DeletePricesRangeAsync(req.PriceIds, ct);
            return new { DeletedCount = count };
        }
    }


}
