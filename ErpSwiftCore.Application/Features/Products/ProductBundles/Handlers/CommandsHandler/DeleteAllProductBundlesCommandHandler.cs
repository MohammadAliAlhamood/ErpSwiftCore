using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class DeleteAllProductBundlesCommandHandler : BaseHandler<DeleteAllProductBundlesCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public DeleteAllProductBundlesCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<DeleteAllProductBundlesCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteAllProductBundlesCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.DeleteAllBundlesAsync(cancellationToken);
            return new { DeletedAll = success };
        }
    }


}
