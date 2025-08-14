using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{
    public class SoftDeleteProductCommandHandler : BaseHandler<SoftDeleteProductCommand>
    {
        private readonly IProductCommandService _svc; 
        public SoftDeleteProductCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<SoftDeleteProductCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        } 
        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductCommand req, CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteProductAsync(req.Dto.ProductId, ct);
            return new { Success = ok };
        }
    } 
}
