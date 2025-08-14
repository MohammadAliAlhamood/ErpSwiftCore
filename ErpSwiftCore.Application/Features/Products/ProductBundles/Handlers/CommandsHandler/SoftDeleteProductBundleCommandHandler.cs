using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class SoftDeleteProductBundleCommandHandler : BaseHandler<SoftDeleteProductBundleCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public SoftDeleteProductBundleCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<SoftDeleteProductBundleCommandHandler> logger) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductBundleCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.SoftDeleteBundleAsync(request.BundleId, cancellationToken);
            return new { SoftDeleted = success };
        }
    } 
}
