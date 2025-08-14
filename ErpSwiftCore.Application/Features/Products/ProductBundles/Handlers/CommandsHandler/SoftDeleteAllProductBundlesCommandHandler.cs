using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{

    public class SoftDeleteAllProductBundlesCommandHandler : BaseHandler<SoftDeleteAllProductBundlesCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public SoftDeleteAllProductBundlesCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<SoftDeleteAllProductBundlesCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(SoftDeleteAllProductBundlesCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.SoftDeleteAllBundlesAsync(cancellationToken);
            return new { SoftDeletedAll = success };
        }
    } 
}
