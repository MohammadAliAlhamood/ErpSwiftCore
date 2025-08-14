using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{ 
    public class DeleteProductsRangeCommandHandler : BaseHandler<DeleteProductsRangeCommand>
    {
        private readonly IProductCommandService _svc;

        public DeleteProductsRangeCommandHandler(
            IProductCommandService svc,
            ILogger<BaseHandler<DeleteProductsRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        } 
        protected override async Task<object?> HandleRequestAsync(DeleteProductsRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteProductsRangeAsync(req.Dto.ProductIds, ct);
            return new { Success = ok };
        }
    } 
}
