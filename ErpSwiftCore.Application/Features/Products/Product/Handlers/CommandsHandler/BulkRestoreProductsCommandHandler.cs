using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{

    public class BulkRestoreProductsCommandHandler : BaseHandler<BulkRestoreProductsCommand>
    {
        private readonly IProductCommandService _svc;

        public BulkRestoreProductsCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<BulkRestoreProductsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(BulkRestoreProductsCommand req, CancellationToken ct)
        {
            var count = await _svc.BulkRestoreProductsAsync(req.Dto.ProductIds, ct);
            return new { RestoredCount = count };
        }
    }


}
