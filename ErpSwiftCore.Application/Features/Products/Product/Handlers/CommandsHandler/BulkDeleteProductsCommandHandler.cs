using Microsoft.Extensions.Logging;
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{

    public class BulkDeleteProductsCommandHandler : BaseHandler<BulkDeleteProductsCommand>
    {
        private readonly IProductCommandService _svc;

        public BulkDeleteProductsCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<BulkDeleteProductsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(BulkDeleteProductsCommand req, CancellationToken ct)
        {
            var count = await _svc.BulkDeleteProductsAsync(req.Dto.ProductIds, ct);
            return new { DeletedCount = count };
        }
    }

}
