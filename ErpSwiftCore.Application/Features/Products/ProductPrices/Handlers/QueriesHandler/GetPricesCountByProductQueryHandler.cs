using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPricesCountByProductQueryHandler : BaseHandler<GetPricesCountByProductQuery>
    {
        private readonly IProductPriceQueryService _svc;

        public GetPricesCountByProductQueryHandler(
            IProductPriceQueryService svc,
            ILogger<BaseHandler<GetPricesCountByProductQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetPricesCountByProductQuery req, CancellationToken ct)
        {
            return await _svc.GetPricesCountByProductAsync(req.ProductId, ct);
        }
    }


}
