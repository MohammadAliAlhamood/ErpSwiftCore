using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{
    public class RestoreProductCommandHandler : BaseHandler<RestoreProductCommand>
    {
        private readonly IProductCommandService _svc;

        public RestoreProductCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<RestoreProductCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreProductCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreProductAsync(req.Dto.ProductId, ct);
            return new { Success = ok };
        }
    }

}
