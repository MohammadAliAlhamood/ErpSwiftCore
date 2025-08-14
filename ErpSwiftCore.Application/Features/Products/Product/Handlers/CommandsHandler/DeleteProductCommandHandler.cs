using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{
    public class DeleteProductCommandHandler : BaseHandler<DeleteProductCommand>
    {
        private readonly IProductCommandService _svc;

        public DeleteProductCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<DeleteProductCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteProductCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteProductAsync(req.Dto.ProductId, ct);
            return new { Success = ok };
        }
    }
}



