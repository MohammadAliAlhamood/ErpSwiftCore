using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{
    public class BulkUpdateStockCommandHandler : BaseHandler<BulkUpdateStockCommand>
    {
        private readonly IProductCommandService _svc;

        public BulkUpdateStockCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<BulkUpdateStockCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(BulkUpdateStockCommand req, CancellationToken ct)
        {
            var count = await _svc.BulkUpdateStockAsync((IEnumerable<(Guid productId, int quantity)>)req.Dto.StockUpdates, ct);
            return new { UpdatedCount = count };
        }
    }


}
