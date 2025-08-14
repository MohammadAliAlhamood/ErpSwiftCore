using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{
    public class BulkSoftDeleteProductsCommandHandler : BaseHandler<BulkSoftDeleteProductsCommand>
    {
        private readonly IProductCommandService _svc;

        public BulkSoftDeleteProductsCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<BulkSoftDeleteProductsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(BulkSoftDeleteProductsCommand req, CancellationToken ct)
        {
            var count = await _svc.BulkSoftDeleteProductsAsync(req.Dto.ProductIds, ct);
            return new { SoftDeletedCount = count };
        }
    }


}
