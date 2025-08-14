using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class SoftDeleteProductBundlesRangeCommandHandler : BaseHandler<SoftDeleteProductBundlesRangeCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public SoftDeleteProductBundlesRangeCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<SoftDeleteProductBundlesRangeCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductBundlesRangeCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.SoftDeleteBundlesRangeAsync(request.BundleIds, cancellationToken);
            return new { SoftDeletedRange = success };
        }
    }



}
