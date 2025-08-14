using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class DeleteProductPriceCommandHandler : BaseHandler<DeleteProductPriceCommand>
    {
        private readonly IProductPriceCommandService _svc;

        public DeleteProductPriceCommandHandler(
            IProductPriceCommandService svc,
            ILogger<BaseHandler<DeleteProductPriceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteProductPriceCommand req, CancellationToken ct)
        {
            await _svc.DeletePriceAsync(req.PriceId, ct);
            return new { DeletedId = req.PriceId };
        }
    }



}
