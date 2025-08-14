using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class RestoreProductBundleCommandHandler : BaseHandler<RestoreProductBundleCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public RestoreProductBundleCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<RestoreProductBundleCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreProductBundleCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.RestoreBundleAsync(request.BundleId, cancellationToken);
            return new { Restored = success };
        }
    }


}
