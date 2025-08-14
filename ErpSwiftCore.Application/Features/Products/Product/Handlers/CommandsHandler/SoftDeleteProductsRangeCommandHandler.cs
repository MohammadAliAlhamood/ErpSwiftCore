using Microsoft.Extensions.Logging;
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{

    public class SoftDeleteProductsRangeCommandHandler : BaseHandler<SoftDeleteProductsRangeCommand>
    {
        private readonly IProductCommandService _svc;
        public SoftDeleteProductsRangeCommandHandler(IProductCommandService svc, ILogger<BaseHandler<SoftDeleteProductsRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(SoftDeleteProductsRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteProductsRangeAsync(req.Dto.ProductIds, ct);
            return new { Success = ok };
        }
    }
}
