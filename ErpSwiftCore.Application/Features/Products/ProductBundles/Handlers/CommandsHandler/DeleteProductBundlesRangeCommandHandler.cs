using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class DeleteProductBundlesRangeCommandHandler : BaseHandler<DeleteProductBundlesRangeCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public DeleteProductBundlesRangeCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<DeleteProductBundlesRangeCommandHandler> logger) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteProductBundlesRangeCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.DeleteBundlesRangeAsync(request.BundleIds, cancellationToken);
            return new { DeletedRange = success };
        }
    }
}
