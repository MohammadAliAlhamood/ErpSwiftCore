using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class RestoreProductBundlesRangeCommandHandler : BaseHandler<RestoreProductBundlesRangeCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public RestoreProductBundlesRangeCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<RestoreProductBundlesRangeCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreProductBundlesRangeCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.RestoreBundlesRangeAsync(request.BundleIds, cancellationToken);
            return new { RestoredRange = success };
        }
    }



}
