using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class DeleteAllProductPricesCommandHandler :
        BaseHandler<DeleteAllProductPricesCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public DeleteAllProductPricesCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<DeleteAllProductPricesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteAllProductPricesCommand req, CancellationToken ct)
        {
            var count = await _svc.DeleteAllPricesAsync(ct);
            return new { DeletedCount = count };
        }
    }



}
