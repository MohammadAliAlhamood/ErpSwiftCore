using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class BulkRestoreProductBundlesCommandHandler : BaseHandler<BulkRestoreProductBundlesCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public BulkRestoreProductBundlesCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<BulkRestoreProductBundlesCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(BulkRestoreProductBundlesCommand request, CancellationToken cancellationToken)
        {
            var count = await _commandService.BulkRestoreBundlesAsync(request.BundleIds, cancellationToken);
            return new { RestoredCount = count };
        }
    }
}
