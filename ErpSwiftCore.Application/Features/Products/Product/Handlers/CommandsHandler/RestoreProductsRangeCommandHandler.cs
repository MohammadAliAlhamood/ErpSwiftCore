using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{
    public class RestoreProductsRangeCommandHandler : BaseHandler<RestoreProductsRangeCommand>
    {
        private readonly IProductCommandService _svc;

        public RestoreProductsRangeCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<RestoreProductsRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        } 
        protected override async Task<object?> HandleRequestAsync(RestoreProductsRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreProductsRangeAsync(req.Dto.ProductIds, ct);
            return new { Success = ok };
        }
    }

}
