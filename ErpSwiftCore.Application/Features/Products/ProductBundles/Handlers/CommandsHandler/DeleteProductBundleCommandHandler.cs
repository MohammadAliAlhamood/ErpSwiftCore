using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class DeleteProductBundleCommandHandler : BaseHandler<DeleteProductBundleCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public DeleteProductBundleCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<DeleteProductBundleCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteProductBundleCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.DeleteBundleAsync(request.BundleId, cancellationToken);
            return new { Deleted = success };
        }
    }


}
