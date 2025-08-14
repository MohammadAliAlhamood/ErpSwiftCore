using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{

    public class BulkSoftDeleteProductBundlesCommandHandler : BaseHandler<BulkSoftDeleteProductBundlesCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public BulkSoftDeleteProductBundlesCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<BulkSoftDeleteProductBundlesCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(BulkSoftDeleteProductBundlesCommand request, CancellationToken cancellationToken)
        {
            var count = await _commandService.BulkSoftDeleteBundlesAsync(request.BundleIds, cancellationToken);
            return new { SoftDeletedCount = count };
        }
    }


}
